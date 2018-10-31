using System.Collections.Generic;
using System.Linq;
using Adamant.Tools.Compiler.Bootstrap.Core.Diagnostics;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Analysis.Declarations.Function;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Analysis.Expressions;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Analysis.Statements;
using Adamant.Tools.Compiler.Bootstrap.Semantics.ControlFlow.Graph;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Declarations;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Names;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Types;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Declarations.Functions;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics.Analysis.Declarations
{
    public class FunctionDeclarationAnalysis : MemberDeclarationAnalysis
    {
        [NotNull] public new NamedFunctionDeclarationSyntax Syntax { get; }
        [NotNull] [ItemNotNull] public IReadOnlyList<ParameterAnalysis> Parameters { get; }
        public int Arity => Parameters.Count;
        [NotNull] public ExpressionAnalysis ReturnTypeExpression { get; }
        [CanBeNull] public DataType ReturnType { get; set; }
        [NotNull] [ItemNotNull] public IReadOnlyList<StatementAnalysis> Statements { get; }
        [CanBeNull] public ControlFlowGraph ControlFlow { get; set; }
        [CanBeNull] public new DataType Type { get; set; }

        public FunctionDeclarationAnalysis(
            [NotNull] AnalysisContext context,
            [NotNull] NamedFunctionDeclarationSyntax syntax,
            [NotNull] QualifiedName name,
            [NotNull] [ItemNotNull] IEnumerable<GenericParameterAnalysis> genericParameters,
            [NotNull] [ItemNotNull] IEnumerable<ParameterAnalysis> parameters,
            [NotNull] ExpressionAnalysis returnTypeExpression,
            [CanBeNull] [ItemNotNull] IEnumerable<StatementAnalysis> statements)
            : base(context, syntax, name, genericParameters)
        {
            Requires.NotNull(nameof(syntax), syntax);
            Requires.NotNull(nameof(returnTypeExpression), returnTypeExpression);
            Syntax = syntax;
            Parameters = parameters.ToReadOnlyList();
            ReturnTypeExpression = returnTypeExpression;
            Statements = (statements ?? Enumerable.Empty<StatementAnalysis>()).ToReadOnlyList();
        }

        [CanBeNull]
        protected override DataType GetDataType()
        {
            return Type;
        }

        [CanBeNull]
        public override Declaration Complete([NotNull] DiagnosticsBuilder diagnostics)
        {
            if (CompleteDiagnostics(diagnostics)) return null;
            return new FunctionDeclaration(
                Context.File,
                Name,
                Parameters.Select(p => p.Complete()),
                ReturnType.AssertKnown(),
                ControlFlow);
        }
    }
}
