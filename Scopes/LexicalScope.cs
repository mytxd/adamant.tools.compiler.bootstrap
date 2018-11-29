using System.Collections.Generic;
using System.Linq;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Metadata.Symbols;
using Adamant.Tools.Compiler.Bootstrap.Names;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Scopes
{
    public abstract class LexicalScope
    {
        [NotNull] private readonly FixedDictionary<SimpleName, ISymbol> symbols;

        protected LexicalScope([NotNull, ItemNotNull] IEnumerable<ISymbol> symbols)
        {
            this.symbols = symbols.ToDictionary(s => s.FullName.UnqualifiedName, s => s)
                .ToFixedDictionary();
        }

        [CanBeNull]
        public virtual ISymbol Lookup([NotNull] SimpleName name)
        {
            return symbols.TryGetValue(name, out var declaration) ? declaration : null;
        }

        [CanBeNull]
        public abstract ISymbol LookupGlobal([NotNull] SimpleName name);
    }
}
