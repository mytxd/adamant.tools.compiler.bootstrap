using System.Collections.Generic;
using System.Linq;
using Adamant.Tools.Compiler.Bootstrap.AST;
using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Metadata.Symbols;
using Adamant.Tools.Compiler.Bootstrap.Names;
using Adamant.Tools.Compiler.Bootstrap.Primitives;
using Adamant.Tools.Compiler.Bootstrap.Scopes;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics.NameBinding
{
    public class NameBinder
    {
        // TODO do we need a list of all the namespaces for validating using statements?
        // Gather a list of all the namespaces for validating using statements
        // Also need to account for empty directories?

        [NotNull] private readonly Diagnostics diagnostics;
        [NotNull, ItemNotNull] private readonly FixedList<ISymbol> allSymbols;
        [NotNull] private readonly GlobalScope globalScope;

        public NameBinder(
            [NotNull] Diagnostics diagnostics,
            [NotNull] PackageSyntax packageSyntax,
            [NotNull] FixedDictionary<string, Package> references)
        {
            this.diagnostics = diagnostics;
            allSymbols = GetAllSymbols(packageSyntax, references);
            globalScope = new GlobalScope(GetAllGlobalSymbols());
        }

        [NotNull]
        private static FixedList<ISymbol> GetAllSymbols(
            [NotNull] PackageSyntax packageSyntax,
            [NotNull] FixedDictionary<string, Package> references)
        {
            return references.Values.NotNull()
                    .SelectMany(p => p.Declarations).NotNull().Cast<ISymbol>()
                    .Concat(packageSyntax.CompilationUnits.SelectMany(cu => cu.NotNull().AllNamespacedDeclarations))
                    .ToFixedList();
        }

        [NotNull, ItemNotNull]
        private IEnumerable<ISymbol> GetAllGlobalSymbols()
        {
            return allSymbols.Where(s => s.NotNull().IsGlobal())
                .Concat(PrimitiveSymbols.Instance);
        }

        public void BindNamesInPackage([NotNull] PackageSyntax package)
        {
            foreach (var compilationUnit in package.CompilationUnits)
                BindNamesInCompilationUnit(compilationUnit);
        }

        private void BindNamesInCompilationUnit([NotNull] CompilationUnitSyntax compilationUnit)
        {
            var containingScope = BuildNamespaceScopes(globalScope, compilationUnit.ImplicitNamespaceName);
            containingScope = BuildUsingDirectivesScope(containingScope, compilationUnit.UsingDirectives);
            foreach (var declaration in compilationUnit.Declarations)
                BindNamesInDeclaration(containingScope, declaration, declaringType: null);
        }

        private void BindNamesInDeclaration(
            [NotNull] LexicalScope containingScope,
            [NotNull] DeclarationSyntax declaration,
            [CanBeNull] TypeDeclarationSyntax declaringType)
        {
            var binder = new ExpressionNameBinder(diagnostics, declaration.File);
            var diagnosticCount = diagnostics.Count;
            switch (declaration)
            {
                case NamespaceDeclarationSyntax ns:
                {
                    if (ns.InGlobalNamespace)
                        containingScope = globalScope;

                    containingScope = BuildNamespaceScopes(containingScope, ns.Name);
                    containingScope = BuildUsingDirectivesScope(containingScope, ns.UsingDirectives);
                    foreach (var nestedDeclaration in ns.Declarations)
                        BindNamesInDeclaration(containingScope, nestedDeclaration, declaringType: null);
                }
                break;
                case NamedFunctionDeclarationSyntax function:
                    function.DeclaringType = declaringType;
                    BindNamesInFunctionParameters(containingScope, function, binder);
                    binder.VisitExpression(function.ReturnTypeExpression, containingScope);
                    BindNamesInFunctionBody(containingScope, function, binder);
                    break;
                case OperatorDeclarationSyntax operatorDeclaration:
                    operatorDeclaration.DeclaringType = declaringType;
                    BindNamesInFunctionParameters(containingScope, operatorDeclaration, binder);
                    binder.VisitExpression(operatorDeclaration.ReturnTypeExpression, containingScope);
                    BindNamesInFunctionBody(containingScope, operatorDeclaration, binder);
                    break;
                case ConstructorDeclarationSyntax constructor:
                    constructor.DeclaringType = declaringType;
                    BindNamesInFunctionParameters(containingScope, constructor, binder);
                    BindNamesInFunctionBody(containingScope, constructor, binder);
                    break;
                case InitializerDeclarationSyntax initializer:
                    initializer.DeclaringType = declaringType;
                    BindNamesInFunctionParameters(containingScope, initializer, binder);
                    BindNamesInFunctionBody(containingScope, initializer, binder);
                    break;
                case TypeDeclarationSyntax typeDeclaration:
                    typeDeclaration.DeclaringType = declaringType;
                    // TODO name scope for type declaration
                    foreach (var nestedDeclaration in typeDeclaration.Members)
                        BindNamesInDeclaration(containingScope, nestedDeclaration, typeDeclaration);
                    break;
                case FieldDeclarationSyntax fieldDeclaration:
                    fieldDeclaration.DeclaringType = declaringType;
                    binder.VisitExpression(fieldDeclaration.TypeExpression, containingScope);
                    binder.VisitExpression(fieldDeclaration.Initializer, containingScope);
                    break;
                default:
                    throw NonExhaustiveMatchException.For(declaration);
            }
            if (diagnosticCount != diagnostics.Count)
                declaration.Poison();
        }

        private void BindNamesInFunctionParameters(
            [NotNull] LexicalScope containingScope,
            [NotNull] FunctionDeclarationSyntax function,
            [NotNull] ExpressionNameBinder binder)
        {
            if (function.GenericParameters != null)
                foreach (var parameter in function.GenericParameters)
                    binder.VisitExpression(parameter.TypeExpression, containingScope);

            foreach (var parameter in function.Parameters)
                switch (parameter)
                {
                    case NamedParameterSyntax namedParameter:
                        binder.VisitExpression(namedParameter.TypeExpression, containingScope);
                        break;
                    case SelfParameterSyntax _:
                    case FieldParameterSyntax _:
                        // Nothing to bind
                        break;
                    default:
                        throw NonExhaustiveMatchException.For(parameter);
                }
        }

        private void BindNamesInFunctionBody(
            [NotNull] LexicalScope containingScope,
            [NotNull] FunctionDeclarationSyntax function,
            [NotNull] ExpressionNameBinder binder)
        {
            var symbols = new List<ISymbol>();
            foreach (var parameter in function.Parameters)
                symbols.Add(parameter);

            containingScope = new NestedScope(containingScope, symbols);
            binder.VisitBlock(function.Body, containingScope);
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

            var symbolsInNamespace = allSymbols.Where(s => s.FullName.HasQualifier(name));
            return new NestedScope(containingScope, symbolsInNamespace);
        }

        [NotNull]
        private LexicalScope BuildUsingDirectivesScope(
            [NotNull] LexicalScope containingScope,
            [NotNull, ItemNotNull] FixedList<UsingDirectiveSyntax> usingDirectives)
        {
            if (!usingDirectives.Any()) return containingScope;

            var importedSymbols = usingDirectives
                .SelectMany(d => allSymbols.Where(s => s.FullName.HasQualifier(d.Name)));
            return new NestedScope(containingScope, importedSymbols);
        }
    }
}
