using System.Runtime.CompilerServices;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Scopes;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Declarations;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Expressions;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Parts;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Statements;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics
{
    internal partial class SemanticAnalysis
    {
        [NotNull]
        public const string LexicalScopeAttribute = "LexicalScope";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LexicalScope LexicalScope([NotNull] SyntaxNode syntax)
        {
            return attributes.GetOrAdd(syntax, LexicalScopeAttribute, ComputeLexicalScope);
        }

        [NotNull]
        private LexicalScope ComputeLexicalScope([NotNull] SyntaxNode syntax)
        {
            switch (syntax)
            {
                case CompilationUnitSyntax _:
                    return new LexicalScope(PackageSyntaxSymbol.GlobalNamespace);

                case FunctionDeclarationSyntax function:
                    {
                        var enclosingScope = LexicalScope(Parent(function));
                        var symbol = SyntaxSymbol(function);
                        return new LexicalScope(enclosingScope, symbol);
                    }

                case StatementSyntax _:
                case ExpressionSyntax _:
                case ParameterSyntax _:
                case SeparatedListSyntax<ParameterSyntax> _:
                case SeparatedListSyntax<ExpressionSyntax> _:
                    {
                        // Inherit
                        var enclosingScope = LexicalScope(Parent(syntax));
                        return enclosingScope;
                    }
                default:
                    throw NonExhaustiveMatchException.For(syntax);
            }
        }
    }
}