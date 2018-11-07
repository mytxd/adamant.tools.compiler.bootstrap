using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Tokens;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes
{
    public class IfExpressionSyntax : ExpressionSyntax
    {
        [NotNull] public IfKeywordToken IfKeyword { get; }
        [NotNull] public ExpressionSyntax Condition { get; }
        [NotNull] public ExpressionBlockSyntax ThenBlock { get; }
        [CanBeNull] public ElseClauseSyntax ElseClause { get; }

        public IfExpressionSyntax(
            [NotNull] IfKeywordToken ifKeyword,
            [NotNull] ExpressionSyntax condition,
            [NotNull] ExpressionBlockSyntax thenBlock,
            [CanBeNull] ElseClauseSyntax elseClause)
            : base(TextSpan.Covering(ifKeyword.Span, thenBlock.Span, elseClause?.Span))
        {
            Requires.NotNull(nameof(ifKeyword), ifKeyword);
            Requires.NotNull(nameof(condition), condition);
            Requires.NotNull(nameof(thenBlock), thenBlock);
            IfKeyword = ifKeyword;
            Condition = condition;
            ThenBlock = thenBlock;
            ElseClause = elseClause;
        }
    }
}