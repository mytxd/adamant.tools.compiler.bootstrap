namespace Adamant.Tools.Compiler.Bootstrap.Semantics.IntermediateLanguage
{
    /// <summary>
    /// A statement that exists to evaluate an expression of some kind. Unlike
    /// block terminator statements, execution of the block continues after an
    /// expression statement.
    /// </summary>
    public abstract class ExpressionStatement : Statement
    {
        protected ExpressionStatement(int blockNumber, int number)
            : base(blockNumber, number)
        {
        }
    }
}
