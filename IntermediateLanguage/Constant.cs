using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Metadata.Types;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.IntermediateLanguage
{
    public abstract class Constant : Operand, IValue
    {
        [NotNull] public readonly DataType Type;

        protected Constant([NotNull] DataType type)
        {
            Requires.NotNull(nameof(type), type);
            Type = type;
        }
    }
}
