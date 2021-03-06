using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Names;
using Adamant.Tools.Compiler.Bootstrap.Tokens;

namespace Adamant.Tools.Compiler.Bootstrap.AST
{
    public class EnumStructDeclarationSyntax : TypeDeclarationSyntax
    {
        public FixedList<AttributeSyntax> Attributes { get; }
        public FixedList<IModiferToken> Modifiers { get; }
        public FixedList<ExpressionSyntax> BaseTypes { get; }
        public FixedList<GenericConstraintSyntax> GenericConstraints { get; }
        public FixedList<ExpressionSyntax> Invariants { get; }
        public FixedList<EnumVariantSyntax> Variants { get; }

        public EnumStructDeclarationSyntax(
            CodeFile file,
            FixedList<AttributeSyntax> attributes,
            FixedList<IModiferToken> modifiers,
            Name fullName,
            TextSpan nameSpan,
            FixedList<GenericParameterSyntax> genericParameters,
            FixedList<ExpressionSyntax> baseTypes,
            FixedList<GenericConstraintSyntax> genericConstraints,
            FixedList<ExpressionSyntax> invariants,
            FixedList<EnumVariantSyntax> variants,
            FixedList<MemberDeclarationSyntax> members)
            : base(file, nameSpan, fullName, genericParameters, members)
        {
            Attributes = attributes;
            Modifiers = modifiers;
            BaseTypes = baseTypes;
            GenericConstraints = genericConstraints;
            Invariants = invariants;
            Variants = variants;
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
    }
}
