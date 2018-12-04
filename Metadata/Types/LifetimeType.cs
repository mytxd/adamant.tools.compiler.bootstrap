using Adamant.Tools.Compiler.Bootstrap.Framework;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Metadata.Types
{
    public class LifetimeType : DataType
    {
        [NotNull] public ObjectType Referent { get; }
        [NotNull] public Lifetime Lifetime { get; }
        public override bool IsResolved { get; }
        public bool IsOwned => Lifetime.IsOwned;

        public LifetimeType([NotNull] ObjectType referent, [NotNull] Lifetime lifetime)
        {
            Referent = referent;
            Lifetime = lifetime;
            IsResolved = Referent.IsResolved;
        }

        [NotNull]
        public override string ToString()
        {
            return $"{Referent}${Lifetime}";
        }
    }
}
