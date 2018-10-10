using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics.Names
{
    public abstract class ScopeName : Name
    {
        protected ScopeName([NotNull] string name)
            : base(name)
        {
        }
    }
}