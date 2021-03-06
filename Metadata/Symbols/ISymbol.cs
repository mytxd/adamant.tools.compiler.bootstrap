using Adamant.Tools.Compiler.Bootstrap.Metadata.Types;
using Adamant.Tools.Compiler.Bootstrap.Names;

namespace Adamant.Tools.Compiler.Bootstrap.Metadata.Symbols
{
    public interface ISymbol
    {
        bool MutableBinding { get; }
        Name FullName { get; }
        DataType Type { get; }
        SymbolSet ChildSymbols { get; }
    }
}
