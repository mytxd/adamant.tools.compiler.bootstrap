using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Tokens
{
    internal abstract class IdentifierToken : Token, IIdentifierToken
    {
        [NotNull] public string Value { get; }

        protected IdentifierToken(TextSpan span, [NotNull] string value)
            : base(span)
        {
            Requires.NotNull(nameof(value), value);
            Value = value;
        }

        // Helpful for debugging
        public override string ToString()
        {
            return Value;
        }
    }
}