using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics.Analyses
{
    public class BreakExpressionAnalysis : ExpressionAnalysis
    {
        [NotNull] public new BreakExpressionSyntax Syntax { get; }
        [CanBeNull] public ExpressionAnalysis Expression { get; }

        public BreakExpressionAnalysis(
            [NotNull] AnalysisContext context,
            [NotNull] BreakExpressionSyntax syntax,
            [CanBeNull] ExpressionAnalysis expression)
            : base(context, syntax)
        {
            Syntax = syntax;
            Expression = expression;
        }
    }
}