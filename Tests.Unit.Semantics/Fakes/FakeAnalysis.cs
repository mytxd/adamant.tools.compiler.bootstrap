using Adamant.Tools.Compiler.Bootstrap.Semantics.Analysis;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Analysis.Expressions;
using Adamant.Tools.Compiler.Bootstrap.Tests.Unit.Fakes;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Tests.Unit.Semantics.Fakes
{
    public static class FakeAnalysis
    {
        [NotNull]
        public static AnalysisContext Analysis()
        {
            return new AnalysisContext("".ToFakeCodeFile(), new FakeLexicalScope());
        }

        [NotNull]
        public static ExpressionAnalysis Expression()
        {
            return new FakeExpression();
        }

        private class FakeExpression : ExpressionAnalysis
        {
            public FakeExpression()
                : base(Analysis(), FakeSyntax.Expression())
            {
            }
        }
    }
}