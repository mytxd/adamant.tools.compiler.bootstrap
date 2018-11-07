using Adamant.Tools.Compiler.Bootstrap.Syntax.Tokens;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes
{
    public class IdentifierNameSyntax : SimpleNameSyntax
    {
        public IdentifierNameSyntax([NotNull] IIdentifierToken name)
            : base(name, name.Span)
        {
        }
    }
}