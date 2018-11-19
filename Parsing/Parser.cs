using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Lexing;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Parsing
{
    public partial class Parser
    {
        [NotNull] protected readonly CodeFile File;
        [NotNull] protected readonly ITokenIterator Tokens;
        [NotNull] private readonly IListParser listParser;

        public Parser(
            [NotNull] ITokenIterator tokens,
            [NotNull] IListParser listParser)
        {
            File = tokens.Context.File;
            Tokens = tokens;
            this.listParser = listParser;
        }

        protected void Add([NotNull] Diagnostic diagnostic)
        {
            Tokens.Context.Diagnostics.Add(diagnostic);
        }
    }
}
