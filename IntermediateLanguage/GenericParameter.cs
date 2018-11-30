using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Metadata.Types;
using Adamant.Tools.Compiler.Bootstrap.Names;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.IntermediateLanguage
{
    public class GenericParameter
    {
        [NotNull] public Name Name { get; }
        [NotNull] public DataType Type { get; internal set; }

        public GenericParameter(
            [NotNull] Name name,
            [NotNull] DataType type)
        {
            Requires.NotNull(nameof(name), name);
            Requires.NotNull(nameof(type), type);
            Name = name;
            Type = type;
        }
    }
}