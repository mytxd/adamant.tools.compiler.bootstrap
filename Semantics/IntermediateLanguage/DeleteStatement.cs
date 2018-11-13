using Adamant.Tools.Compiler.Bootstrap.Core;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics.IntermediateLanguage
{
    public class DeleteStatement : ExpressionStatement
    {
        public readonly int VariableNumber;
        public readonly TextSpan Span;

        public DeleteStatement(int blockNumber, int number, int variableNumber, TextSpan span)
            : base(blockNumber,number)
        {
            VariableNumber = variableNumber;
            Span = span;
        }

        // Useful for debugging
        public override string ToString()
        {
            return $"delete {VariableNumber};";
        }
    }
}
