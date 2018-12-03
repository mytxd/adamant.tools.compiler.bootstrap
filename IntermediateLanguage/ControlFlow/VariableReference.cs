namespace Adamant.Tools.Compiler.Bootstrap.IntermediateLanguage.ControlFlow
{
    public class VariableReference : Place
    {
        public readonly int VariableNumber;

        public VariableReference(int variableNumber)
        {
            VariableNumber = variableNumber;
        }

        public override string ToString()
        {
            var variableName = VariableNumber == 0 ? "result" : VariableNumber.ToString();
            return $"%{variableName}";
        }

        public override int CoreVariable()
        {
            return VariableNumber;
        }
    }
}
