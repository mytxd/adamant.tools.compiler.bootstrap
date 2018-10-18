using Adamant.Tools.Compiler.Bootstrap.Semantics.Statements.LValues;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Types;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics.ControlFlow.Graph
{
    public class LocalVariableDeclaration
    {
        public readonly int Number; // The declaration number is used as its name in the IR

        // If this declaration corresponds to an argument or local variable, what it was named. Not guaranteed unique
        [CanBeNull] public string Name = null;
        public readonly bool MutableBinding;
        [NotNull] public readonly DataType Type;
        [NotNull] public readonly VariableReference Reference;

        public LocalVariableDeclaration(bool mutableBinding, [NotNull] DataType type, int number)
        {
            Number = number;
            MutableBinding = mutableBinding;
            Type = type;
            Reference = new VariableReference(number);
        }
    }
}