using System;

namespace Adamant.Tools.Compiler.Bootstrap.Core.Diagnostics
{
    public class Diagnostic
    {
        public readonly CodeFile File;
        public readonly TextSpan Span;
        public readonly TextPosition StartPosition;
        public readonly TextPosition EndPosition;
        public readonly DiagnosticLevel Level;
        public readonly DiagnosticPhase Phase;
        public readonly int ErrorCode;
        public readonly string Message;

        public Diagnostic(
            CodeFile file,
            TextSpan span,
            DiagnosticLevel level,
            DiagnosticPhase phase,
            int errorCode,
            string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("message", nameof(message));

            Requires.ValidEnum(nameof(level), level);
            Requires.ValidEnum(nameof(phase), phase);

            File = file;
            Span = span;
            StartPosition = file.Code.PositionOfStart(span);
            EndPosition = file.Code.PositionOfEnd(span);
            Level = level;
            Phase = phase;
            ErrorCode = errorCode;
            Message = message;
        }
    }
}
