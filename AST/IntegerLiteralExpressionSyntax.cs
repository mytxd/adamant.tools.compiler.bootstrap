using System.Numerics;
using Adamant.Tools.Compiler.Bootstrap.Core;

namespace Adamant.Tools.Compiler.Bootstrap.AST
{
    public class IntegerLiteralExpressionSyntax : LiteralExpressionSyntax
    {
        public BigInteger Value { get; }

        public IntegerLiteralExpressionSyntax(TextSpan span, BigInteger value)
            : base(span)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
