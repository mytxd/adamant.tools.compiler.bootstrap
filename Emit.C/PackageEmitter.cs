using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Tools.Compiler.Bootstrap.IntermediateLanguage;
using Adamant.Tools.Compiler.Bootstrap.Metadata.Types;
using Adamant.Tools.Compiler.Bootstrap.Names;
using Adamant.Tools.Compiler.Bootstrap.Semantics;

namespace Adamant.Tools.Compiler.Bootstrap.Emit.C
{
    public class PackageEmitter : IEmitter<Package>
    {
        private readonly NameMangler nameMangler;
        private readonly IEmitter<Declaration> declarationEmitter;

        public PackageEmitter(
            NameMangler nameMangler,
            IEmitter<Declaration> declarationEmitter)
        {
            this.nameMangler = nameMangler;
            this.declarationEmitter = declarationEmitter;
        }

        public void Emit(Package package, Code code)
        {
            foreach (var declaration in package.Declarations)
                declarationEmitter.Emit(declaration, code);

            EmitEntryPointAdapter(package.EntryPoint, code);
        }

        public void EmitPreamble(Code code)
        {
            // Setup the beginning of each section
            code.Includes.AppendLine($"#include \"{CodeEmitter.RuntimeLibraryHeaderFileName}\"");

            code.TypeIdDeclaration.AppendLine("// Type ID Declarations");
            // Type ID Enum
            code.TypeIdDeclaration.AppendLine("enum _Type_ID");
            code.TypeIdDeclaration.BeginBlock();
            code.TypeIdDeclaration.AppendLine("_never = 0,");
            // TODO setup primitive types?

            code.TypeDeclarations.AppendLine("// Type Declarations");
            code.FunctionDeclarations.AppendLine("// Function Declarations");
            code.StructDeclarations.AppendLine("// Struct Declarations");
            code.GlobalDefinitions.AppendLine("// Global Definitions");
            code.Definitions.AppendLine("// Definitions");
        }

        public void EmitEntryPointAdapter(FunctionDeclaration entryPoint, Code code)
        {
            if (entryPoint == null) return;

            code.Definitions.DeclarationSeparatorLine();
            code.Definitions.AppendLine("// Entry Point Adapter");
            code.Definitions.AppendLine("int32_t main(const int argc, char const * const * const argv)");
            code.Definitions.BeginBlock();
            var arguments = new List<string>();
            foreach (var parameterTypeName in entryPoint.Parameters.Select(p => p.Type).Cast<ObjectType>().Select(t => t.Name))
            {
                if (parameterTypeName.Equals(Name.From("system", "console", "Console")))
                {
                    code.Definitions.AppendLine("system__console__Console console = { &system__console__Console___vtable, malloc(sizeof(system__console__Console___Self)) };");
                    arguments.Add("system__console__Console___new__1(console)");
                }
                else if (parameterTypeName.Equals(Name.From("system", "console", "Arguments")))
                    throw new NotImplementedException();
                else
                    throw new Exception($"Unexpected type for parameter to main: {parameterTypeName}");
            }
            var joinedArguments = string.Join(", ", arguments);
            if (entryPoint.ReturnType == DataType.Void)
            {
                code.Definitions.AppendLine($"{nameMangler.MangleName(entryPoint)}({joinedArguments});");
                code.Definitions.AppendLine("return 0;");
            }
            else
                code.Definitions.AppendLine($"return {nameMangler.MangleName(entryPoint)}({joinedArguments})._value;");

            code.Definitions.EndBlock();
        }

        public void EmitPostamble(Code code)
        {
            // Close the Type_ID enum
            code.TypeIdDeclaration.EndBlockWithSemicolon();
            code.TypeIdDeclaration.AppendLine("typedef enum _Type_ID _Type_ID;");
        }
    }
}
