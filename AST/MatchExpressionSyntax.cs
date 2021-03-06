using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Framework;

namespace Adamant.Tools.Compiler.Bootstrap.AST
{
    public class MatchExpressionSyntax : ExpressionSyntax
    {
        public ExpressionSyntax Value { get; }
        public FixedList<MatchArmSyntax> Arms { get; }

        public MatchExpressionSyntax(
            TextSpan span,
            ExpressionSyntax value,
            FixedList<MatchArmSyntax> arms)
            : base(span)
        {
            Value = value;
            Arms = arms;
        }

        public override string ToString()
        {
            return $"match {Value} {{{Arms}}}";
        }
    }
}
