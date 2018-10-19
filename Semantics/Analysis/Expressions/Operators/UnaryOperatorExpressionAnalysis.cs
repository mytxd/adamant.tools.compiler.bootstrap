using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Expressions.Operators;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics.Analysis.Expressions.Operators
{
    public class UnaryOperatorExpressionAnalysis : ExpressionAnalysis
    {
        [NotNull] public new UnaryOperatorExpressionSyntax Syntax { get; }
        [NotNull] public ExpressionAnalysis Operand { get; }

        public UnaryOperatorExpressionAnalysis(
            [NotNull] AnalysisContext context,
            [NotNull] UnaryOperatorExpressionSyntax syntax,
            [NotNull] ExpressionAnalysis operand)
            : base(context, syntax)
        {
            Requires.NotNull(nameof(operand), operand);
            Syntax = syntax;
            Operand = operand;
        }
    }
}