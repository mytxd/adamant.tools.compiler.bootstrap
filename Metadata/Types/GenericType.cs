using Adamant.Tools.Compiler.Bootstrap.Framework;

namespace Adamant.Tools.Compiler.Bootstrap.Metadata.Types
{
    public abstract class GenericType : DataType
    {
        public abstract FixedList<DataType> GenericParameterTypes { get; }
        public int? GenericArity => GenericParameterTypes?.Count;
        public abstract FixedList<DataType> GenericArguments { get; }
    }
}
