using Adamant.Tools.Compiler.Bootstrap.IntermediateLanguage;
using Adamant.Tools.Compiler.Bootstrap.Types;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Emit.C
{
    public class ParameterConverter : IConverter<Parameter>
    {
        [NotNull] private readonly NameMangler nameMangler;
        [NotNull] private readonly IConverter<DataType> typeConverter;

        public ParameterConverter(
            [NotNull] NameMangler nameMangler,
            [NotNull] IConverter<DataType> typeConverter)
        {
            this.typeConverter = typeConverter;
            this.nameMangler = nameMangler;
        }

        public string Convert([NotNull] Parameter parameter)
        {
            var type = typeConverter.Convert(parameter.Type);
            var name = nameMangler.Mangle(parameter.Name.UnqualifiedName);
            return parameter.MutableBinding ? $"{type} {name}" : $"const {type} {name}";
        }
    }
}
