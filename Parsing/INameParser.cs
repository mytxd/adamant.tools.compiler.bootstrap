using Adamant.Tools.Compiler.Bootstrap.AST;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Parsing
{
    // TODO really clarify what names are and when it makes sense to say we are parsing one
    public interface INameParser
    {
        [MustUseReturnValue]
        [NotNull]
        NameSyntax ParseName();
    }
}
