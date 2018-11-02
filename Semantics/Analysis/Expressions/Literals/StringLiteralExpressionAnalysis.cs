using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Expressions.Literals;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics.Analysis.Expressions.Literals
{
    public class StringLiteralExpressionAnalysis : LiteralExpressionAnalysis
    {
        [NotNull] public new StringLiteralExpressionSyntax Syntax { get; }
        public string Value => Syntax.Literal.Value;

        public StringLiteralExpressionAnalysis(
            [NotNull] AnalysisContext context,
            [NotNull] StringLiteralExpressionSyntax syntax)
            : base(context, syntax)
        {
            Syntax = syntax;
        }
    }
}
