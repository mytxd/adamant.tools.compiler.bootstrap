using Adamant.Tools.Compiler.Bootstrap.Core;

namespace Adamant.Tools.Compiler.Bootstrap.AST
{
    public class BinaryExpressionSyntax : ExpressionSyntax
    {
        public ExpressionSyntax LeftOperand { get; }
        public BinaryOperator Operator { get; }
        public ExpressionSyntax RightOperand { get; }

        public BinaryExpressionSyntax(
            ExpressionSyntax leftOperand,
            BinaryOperator @operator,
            ExpressionSyntax rightOperand)
            : base(TextSpan.Covering(leftOperand.Span, rightOperand.Span))
        {
            LeftOperand = leftOperand;
            Operator = @operator;
            RightOperand = rightOperand;
        }

        public override string ToString()
        {
            return $"{LeftOperand} {Operator} {RightOperand}";
        }
    }
}
