using System.Collections.Generic;
using System.Linq;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Metadata.Types;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.IntermediateLanguage.ControlFlow
{
    public class ControlFlowGraph
    {
        [NotNull] [ItemNotNull] public FixedList<LocalVariableDeclaration> VariableDeclarations { get; }
        [NotNull] public LocalVariableDeclaration ReturnVariable => VariableDeclarations[0];
        [NotNull] public DataType ReturnType => ReturnVariable.Type;
        [NotNull] [ItemNotNull] public FixedList<BasicBlock> BasicBlocks { get; }
        [NotNull] public BasicBlock EntryBlock => BasicBlocks.First();
        [NotNull] public BasicBlock ExitBlock => BasicBlocks.Last();
        [NotNull] public Edges Edges { get; }

        public ControlFlowGraph(
            [NotNull] IEnumerable<LocalVariableDeclaration> variableDeclarations,
            [NotNull] IEnumerable<BasicBlock> basicBlocks)
        {
            VariableDeclarations = variableDeclarations.ToFixedList();
            BasicBlocks = basicBlocks.ToFixedList();
            Edges = Edges.InGraph(this);
        }
    }
}
