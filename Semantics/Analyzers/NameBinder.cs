using System.Linq;
using Adamant.Tools.Compiler.Bootstrap.AST;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Names;
using Adamant.Tools.Compiler.Bootstrap.Primitives;
using Adamant.Tools.Compiler.Bootstrap.Scopes;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Symbols;
using Adamant.Tools.Compiler.Bootstrap.Symbols;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics.Analyzers
{
    public class NameBinder
    {
        [NotNull, ItemNotNull] private readonly FixedList<ISymbol> symbols;
        [NotNull] private readonly GlobalScope globalScope;

        public NameBinder(
            [NotNull] PackageSyntax packageSyntax,
            [NotNull] FixedDictionary<string, Package> references)
        {
            symbols = references.Values.NotNull()
                .SelectMany(p => p.Declarations).NotNull().Cast<ISymbol>()
                .Concat(packageSyntax.CompilationUnits.SelectMany(cu => cu.NotNull().AllNonMemberDeclarations))
                .ToFixedList();

            var globalSymbols = symbols.Where(s => s.NotNull().IsGlobal())
                .Concat(PrimitiveSymbols.Instance);

            globalScope = new GlobalScope(globalSymbols);
        }

        public void BindNames([NotNull] PackageSyntax package)
        {
            foreach (var compilationUnit in package.CompilationUnits)
                BindNames(compilationUnit);
        }

        private void BindNames([NotNull] CompilationUnitSyntax compilationUnit)
        {
            var containingScope = BuildNamespaceScopes(globalScope, compilationUnit.ImplicitNamespaceName);
            containingScope = BuildUsingDirectivesScope(containingScope, compilationUnit.UsingDirectives);
            foreach (var declaration in compilationUnit.Declarations)
                BindNames(containingScope, declaration);
        }

        private void BindNames(
            [NotNull] LexicalScope containingScope,
            [NotNull] DeclarationSyntax declaration)
        {
            switch (declaration)
            {
                case NamespaceDeclarationSyntax ns:
                {
                    if (ns.InGlobalNamespace)
                        containingScope = globalScope;

                    containingScope = BuildNamespaceScopes(containingScope, ns.Name);
                    containingScope = BuildUsingDirectivesScope(containingScope, ns.UsingDirectives);
                    foreach (var nestedDeclaration in ns.Declarations)
                        BindNames(containingScope, nestedDeclaration);
                }
                break;
                case NamedFunctionDeclarationSyntax namedFunction:
                {
                    // TODO implement
                }
                break;
                default:
                    throw NonExhaustiveMatchException.For(declaration);
            }
        }

        [NotNull]
        private LexicalScope BuildNamespaceScopes(
            [NotNull] LexicalScope containingScope,
            [NotNull] RootName ns)
        {
            Name name;
            switch (ns)
            {
                case GlobalNamespaceName _:
                    return containingScope;
                case QualifiedName qualifiedName:
                    containingScope = BuildNamespaceScopes(containingScope, qualifiedName.Qualifier);
                    name = qualifiedName;
                    break;
                case SimpleName simpleName:
                    name = simpleName;
                    break;
                default:
                    throw NonExhaustiveMatchException.For(ns);
            }

            var symbolsInNamespace = symbols.Where(s => s.Name.HasQualifier(name));
            return new NestedScope(containingScope, symbolsInNamespace);
        }

        [NotNull]
        private LexicalScope BuildUsingDirectivesScope(
            [NotNull] LexicalScope containingScope,
            [NotNull, ItemNotNull] FixedList<UsingDirectiveSyntax> usingDirectives)
        {
            if (!usingDirectives.Any()) return containingScope;

            var importedSymbols = usingDirectives
                .SelectMany(d => symbols.Where(s => s.Name.HasQualifier(d.Name)));
            return new NestedScope(containingScope, importedSymbols);
        }
    }
}
