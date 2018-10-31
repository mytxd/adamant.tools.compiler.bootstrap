using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Expressions.Blocks;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Patterns;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Tokens;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Expressions.ControlFlow.Match
{
    public class MatchArmSyntax : SyntaxNode
    {
        [NotNull] public PatternSyntax Pattern { get; }
        [NotNull] public ExpressionBlockSyntax Expression { get; }
        [CanBeNull] public CommaToken Comma { get; }

        public MatchArmSyntax(
            [NotNull] PatternSyntax pattern,
            [NotNull] ExpressionBlockSyntax expression,
            [CanBeNull] CommaToken comma)
        {
            Requires.NotNull(nameof(pattern), pattern);
            Requires.NotNull(nameof(expression), expression);
            Pattern = pattern;
            Expression = expression;
            Comma = comma;
        }
    }
}