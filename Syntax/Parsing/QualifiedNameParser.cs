using System.Threading.Tasks;
using Adamant.Tools.Compiler.Bootstrap.Core.Diagnostics;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Lexing;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Expressions.Types.Names;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Tokens;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Syntax.Parsing
{
    public class QualifiedNameParser : IParser<NameSyntax>
    {
        [NotNull] private readonly Task<IExpressionParser> expressionParser;

        public QualifiedNameParser([NotNull] Task<IExpressionParser> expressionParser)
        {
            Requires.NotNull(nameof(expressionParser), expressionParser);
            this.expressionParser = expressionParser;
        }

        [MustUseReturnValue]
        [NotNull]
        public NameSyntax Parse([NotNull] ITokenStream tokens, [NotNull] IDiagnosticsCollector diagnostics)
        {
            NameSyntax name = ParseSimpleName(tokens, diagnostics);
            while (tokens.Current is DotToken)
            {
                var dot = tokens.Take<DotToken>();
                var simpleName = ParseSimpleName(tokens, diagnostics);
                name = new QualifiedNameSyntax(name, dot, simpleName);
            }
            return name;
        }

        [MustUseReturnValue]
        [NotNull]
        private SimpleNameSyntax ParseSimpleName([NotNull]ITokenStream tokens, [NotNull] IDiagnosticsCollector diagnostics)
        {
            var identifier = tokens.ExpectIdentifier();
            SimpleNameSyntax simpleName;
            if (tokens.Current is OpenBracketToken)
            {
                var openBracket = tokens.Expect<IOpenBracketToken>();
                var arguments = expressionParser.Result.ParseArguments(tokens, diagnostics);
                var closeBracket = tokens.Expect<ICloseBracketToken>();
                simpleName = new GenericNameSyntax(identifier, openBracket, arguments, closeBracket);
            }
            else
                simpleName = new IdentifierNameSyntax(identifier);
            return simpleName;
        }
    }
}
