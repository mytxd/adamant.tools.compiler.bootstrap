using System.Diagnostics;

namespace Adamant.Tools.Compiler.Bootstrap.IntermediateLanguage
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public abstract class Operand
    {
        // Useful for debugging
        public abstract override string ToString();
    }
}