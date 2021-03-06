using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Framework;

namespace Adamant.Tools.Compiler.Bootstrap.AST
{
    public class StringLiteralExpressionSyntax : LiteralExpressionSyntax
    {
        public string Value { get; }

        public StringLiteralExpressionSyntax(TextSpan span, string value)
            : base(span)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"\"{Value.Escape()}\"";
        }
    }
}
