using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Metadata.Symbols;
using Adamant.Tools.Compiler.Bootstrap.Metadata.Types;
using Adamant.Tools.Compiler.Bootstrap.Names;

namespace Adamant.Tools.Compiler.Bootstrap.AST
{
    public abstract class TypeDeclarationSyntax : MemberDeclarationSyntax
    {
        public FixedList<GenericParameterSyntax> GenericParameters { get; }
        public FixedList<MemberDeclarationSyntax> Members { get; }
        public Metatype Metatype => (Metatype)Type.Fulfilled();

        protected TypeDeclarationSyntax(
            CodeFile file,
            TextSpan nameSpan,
            Name fullName,
            FixedList<GenericParameterSyntax> genericParameters,
            FixedList<MemberDeclarationSyntax> members)
            : base(file, fullName, nameSpan, new SymbolSet(members))
        {
            Members = members;
            GenericParameters = genericParameters;
            foreach (var member in Members) member.DeclaringType = this;
        }
    }
}
