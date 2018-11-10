using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Tokens;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Syntax
{
    public class ClassDeclarationSyntax : MemberDeclarationSyntax
    {
        [NotNull] public SyntaxList<ModifierSyntax> Modifiers { get; }
        [NotNull] public IClassKeywordToken ClassKeyword { get; }
        [NotNull] public IIdentifierTokenPlace Name { get; }
        [CanBeNull] public GenericParametersSyntax GenericParameters { get; }
        [CanBeNull] public BaseClassSyntax BaseClass { get; }
        [CanBeNull] public BaseTypesSyntax BaseTypes { get; }
        [NotNull] public SyntaxList<GenericConstraintSyntax> GenericConstraints { get; }
        [NotNull] public SyntaxList<InvariantSyntax> Invariants { get; }
        [NotNull] public IOpenBraceTokenPlace OpenBrace { get; }
        [NotNull] public FixedList<MemberDeclarationSyntax> Members { get; }
        [NotNull] public ICloseBraceTokenPlace CloseBrace { get; }

        public ClassDeclarationSyntax(
            [NotNull] SyntaxList<ModifierSyntax> modifiers,
            [NotNull] IClassKeywordToken classKeyword,
            [NotNull] IIdentifierTokenPlace name,
            [CanBeNull] GenericParametersSyntax genericParameters,
            [CanBeNull] BaseClassSyntax baseClass,
            [CanBeNull] BaseTypesSyntax baseTypes,
            [NotNull] SyntaxList<GenericConstraintSyntax> genericConstraints,
            [NotNull] SyntaxList<InvariantSyntax> invariants,
            [NotNull] IOpenBraceTokenPlace openBrace,
            [NotNull] FixedList<MemberDeclarationSyntax> members,
            [NotNull] ICloseBraceTokenPlace closeBrace)
            : base(TextSpan.Covering(classKeyword.Span, name.Span))
        {
            Requires.NotNull(nameof(modifiers), modifiers);
            Requires.NotNull(nameof(classKeyword), classKeyword);
            Requires.NotNull(nameof(name), name);
            Requires.NotNull(nameof(genericConstraints), genericConstraints);
            Requires.NotNull(nameof(invariants), invariants);
            Requires.NotNull(nameof(openBrace), openBrace);
            Requires.NotNull(nameof(members), members);
            Requires.NotNull(nameof(closeBrace), closeBrace);
            Modifiers = modifiers;
            ClassKeyword = classKeyword;
            Name = name;
            GenericParameters = genericParameters;
            BaseClass = baseClass;
            BaseTypes = baseTypes;
            GenericConstraints = genericConstraints;
            Invariants = invariants;
            OpenBrace = openBrace;
            Members = members;
            CloseBrace = closeBrace;
        }
    }
}
