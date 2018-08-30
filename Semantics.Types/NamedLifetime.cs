namespace Adamant.Tools.Compiler.Bootstrap.Semantics.Types
{
    public class NamedLifetime : Lifetime
    {
        public readonly string Name;

        public NamedLifetime(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
