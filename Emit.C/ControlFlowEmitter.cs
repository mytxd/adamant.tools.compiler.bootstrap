using System.Linq;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Semantics.ControlFlow;
using Adamant.Tools.Compiler.Bootstrap.Semantics.ControlFlow.Graph;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Statements;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Statements.LValues;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Types;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Emit.C
{
    public class ControlFlowEmitter : IEmitter<ControlFlowGraph>
    {
        [NotNull] private readonly NameMangler nameMangler;
        [NotNull] private readonly IConverter<DataType> typeConverter;

        public ControlFlowEmitter(
            [NotNull] NameMangler nameMangler,
            [NotNull] IConverter<DataType> typeConverter)
        {
            this.typeConverter = typeConverter;
            this.nameMangler = nameMangler;
        }

        public void Emit([NotNull] ControlFlowGraph cfg, [NotNull] Code code)
        {
            var definitions = code.Definitions;

            foreach (var variable in cfg.VariableDeclarations)
                Emit(variable, definitions);

            if (cfg.VariableDeclarations.Any(v => v.Type != ObjectType.Void))
                definitions.BlankLine();

            // TODO assign parameters to temp variables?

            var voidReturn = cfg.ReturnType == ObjectType.Void;
            foreach (var block in cfg.BasicBlocks)
                Emit(block, voidReturn, definitions);
        }

        private void Emit([NotNull] LocalVariableDeclaration variable, [NotNull] CCodeBuilder code)
        {
            // Skip void variables
            if (variable.Type == ObjectType.Void) return;

            var initializer = variable.Name != null ? $" = ᵢ{nameMangler.Mangle(variable.Name)}" : "";
            code.AppendLine($"{typeConverter.Convert(variable.Type)} ₜ{NameOf(variable.Reference)}{initializer};");
        }

        private static string NameOf([NotNull] VariableReference variable)
        {
            return variable.VariableNumber == 0 ? "result" : variable.VariableNumber.ToString();
        }

        private static void Emit([NotNull] BasicBlock block, bool voidReturn, [NotNull] CCodeBuilder code)
        {
            code.AppendLine($"bb{block.Number}:");
            code.BeginBlock();
            foreach (var statement in block.Statements)
                switch (statement)
                {
                    case AddStatement a:
                        // TODO this should emit something that can do a checked add
                        code.AppendLine($"{Convert(a.LValue)} = (ₐint){{{Convert(a.LeftOperand)}.ₐvalue + {Convert(a.RightOperand)}.ₐvalue}};");
                        break;
                    case IntegerLiteralStatement s:
                        // TODO this could be of a different type
                        code.AppendLine($"{Convert(s.LValue)} = (ₐint){{{s.Value}}};");
                        break;
                    case ReturnStatement _:
                        code.AppendLine(voidReturn ? "return;" : "return ₜresult;");
                        break;
                    default:
                        throw NonExhaustiveMatchException.For(statement);
                }
            code.EndBlock();
        }

        private static string Convert(LValue lValue)
        {
            switch (lValue)
            {
                case VariableReference variable:
                    return "ₜ" + NameOf(variable);
                default:
                    throw NonExhaustiveMatchException.For(lValue);
            }
        }
    }
}