using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Expressions;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics.Scopes
{
    public class BlockScope : NestedScope
    {
        [NotNull] public new BlockExpressionSyntax Syntax { get; }

        public BlockScope(
            [NotNull] LexicalScope containingScope,
            [NotNull] BlockExpressionSyntax syntax)
            : base(containingScope, syntax)
        {
            Syntax = syntax;
        }
    }
}