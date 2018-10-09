using Adamant.Tools.Compiler.Bootstrap.Syntax.Lexing;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Expressions.Types.Names;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Tokens;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Syntax.Parsing
{
    public class QualifiedNameParser : IParser<NameSyntax>
    {
        [MustUseReturnValue]
        [NotNull]
        public NameSyntax Parse([NotNull] ITokenStream tokens)
        {
            NameSyntax qualifiedName = new IdentifierNameSyntax(tokens.ExpectIdentifier());
            while (tokens.Current is DotToken)
            {
                var dot = tokens.Expect<DotToken>();
                var name = new IdentifierNameSyntax(tokens.ExpectIdentifier());
                qualifiedName = new QualifiedNameSyntax(qualifiedName, dot, name);
            }
            return qualifiedName;
        }
    }
}