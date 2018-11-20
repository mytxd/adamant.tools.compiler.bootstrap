using Adamant.Tools.Compiler.Bootstrap.AST;
using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Tokens;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Parsing
{
    public partial class Parser
    {
        [MustUseReturnValue]
        [NotNull]
        public NameSyntax ParseName()
        {
            NameSyntax name = ParseSimpleName();
            while (Tokens.Accept<IDotToken>())
            {
                var simpleName = ParseSimpleName();
                name = new QualifiedNameSyntax(name, simpleName);
            }
            return name;
        }

        [MustUseReturnValue]
        [NotNull]
        private SimpleNameSyntax ParseSimpleName()
        {
            var identifier = Tokens.RequiredToken<IIdentifierToken>();
            SimpleNameSyntax simpleName;
            if (Tokens.Accept<IOpenBracketToken>())
            {
                var arguments = ParseArguments();
                var closeBracket = Tokens.Expect<ICloseBracketToken>();
                var span = TextSpan.Covering(identifier.Span, closeBracket);
                simpleName = new GenericNameSyntax(span, identifier.Value, arguments);
            }
            else
                simpleName = new IdentifierNameSyntax(identifier.Span, identifier.Value);
            return simpleName;
        }
    }
}