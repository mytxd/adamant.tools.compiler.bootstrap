using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics.Types
{
    public abstract class Lifetime
    {
        public abstract bool IsOwned { get; }
        [NotNull] public abstract override string ToString();
    }
}
