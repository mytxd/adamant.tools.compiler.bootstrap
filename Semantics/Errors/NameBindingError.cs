using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Core.Diagnostics;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Tokens.Identifiers;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics.Errors
{
    /// <summary>
    /// Error Code Ranges:
    /// 1001-1999: Lexical Errors
    /// 2001-2999: Parsing Errors
    /// 3001-3999: Type Errors
    /// 4001-4999: Borrow Checking Errors
    /// 5001-5999: Name Binding Errors
    /// </summary>
    public static class NameBindingError
    {
        [NotNull]
        public static Diagnostic CouldNotBindName([NotNull] CodeFile file, [NotNull] IIdentifierToken name)
        {
            return new Diagnostic(file, name.Span, DiagnosticLevel.FatalCompilationError, DiagnosticPhase.Analysis, 5001, $"The name `{name.Value}` is not defined in this scope.");
        }
    }
}
