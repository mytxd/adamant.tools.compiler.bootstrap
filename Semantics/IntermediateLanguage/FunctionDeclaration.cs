using System.Collections.Generic;
using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Names;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Types;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics.IntermediateLanguage
{
    public class FunctionDeclaration : Declaration
    {
        [NotNull, ItemNotNull] public IReadOnlyList<Parameter> Parameters { get; }
        public int Arity => Parameters.Count;
        [NotNull] public DataType ReturnType { get; }
        [CanBeNull] public ControlFlowGraph ControlFlow { get; }

        public FunctionDeclaration(
            [NotNull] CodeFile file,
            [NotNull] Name name,
            [NotNull] DataType type,
            [NotNull, ItemNotNull] IEnumerable<Parameter> parameters,
            [NotNull] DataType returnType,
            [CanBeNull] ControlFlowGraph controlFlow)
            : base(file, name, type)
        {
            Requires.NotNull(nameof(parameters), parameters);
            Requires.NotNull(nameof(returnType), returnType);
            Parameters = parameters.ToReadOnlyList();
            ReturnType = returnType;
            ControlFlow = controlFlow;
        }
    }
}