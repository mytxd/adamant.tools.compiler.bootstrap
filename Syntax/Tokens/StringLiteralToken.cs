using System.Collections.Generic;
using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Core.Diagnostics;
using Core.Syntax;

namespace Adamant.Tools.Compiler.Bootstrap.Syntax.Tokens
{
    public class StringLiteralToken : Token
    {
        public readonly string Value;

        public StringLiteralToken(CodeText code, TextSpan span, bool isMissing, string value, IEnumerable<DiagnosticInfo> diagnosticInfos)
            : base(code, span, TokenKind.StringLiteral, isMissing, diagnosticInfos)
        {
            Value = value;
        }
    }
}
