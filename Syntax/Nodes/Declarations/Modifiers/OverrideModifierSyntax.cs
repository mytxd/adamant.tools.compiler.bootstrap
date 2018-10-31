using System.Collections.Generic;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Tokens;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Declarations.Modifiers
{
    public class OverrideModifierSyntax : ModifierSyntax
    {
        [NotNull] public OverrideKeywordToken OverrideKeyword { get; }

        public OverrideModifierSyntax([NotNull] OverrideKeywordToken overrideKeyword)
        {
            Requires.NotNull(nameof(overrideKeyword), overrideKeyword);
            OverrideKeyword = overrideKeyword;
        }

        public override IEnumerable<IToken> Tokens()
        {
            yield return OverrideKeyword;
        }
    }
}
