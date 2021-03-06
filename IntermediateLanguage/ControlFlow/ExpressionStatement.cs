using Adamant.Tools.Compiler.Bootstrap.Core;

namespace Adamant.Tools.Compiler.Bootstrap.IntermediateLanguage.ControlFlow
{
    /// <summary>
    /// A statement that exists to evaluate an expression of some kind. Unlike
    /// block terminator statements, execution of the block continues after an
    /// expression statement.
    /// </summary>
    public abstract class ExpressionStatement : Statement
    {
        public TextSpan Span { get; }

        protected ExpressionStatement(TextSpan span)
        {
            Span = span;
        }
    }
}
