using Adamant.Tools.Compiler.Bootstrap.Core;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Syntax.Tokens
{
    public class BareIdentifierToken : IdentifierToken
    {
        public BareIdentifierToken(TextSpan span, [NotNull] string value)
            : base(span, value)
        {
        }
    }
}