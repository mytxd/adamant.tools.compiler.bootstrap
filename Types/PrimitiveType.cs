using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Names;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Types
{
    public abstract class PrimitiveType : DataType
    {
        [NotNull] public Name Name { get; }

        protected PrimitiveType([NotNull] string name)
        {
            Requires.NotNull(nameof(name), name);
            Name = SimpleName.Special(name);
        }
    }
}
