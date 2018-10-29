using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Declarations.Generic;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Declarations.Modifiers;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Tokens;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Declarations
{
    public class TypeDeclarationSyntax : MemberDeclarationSyntax
    {
        [NotNull] public SyntaxList<ModifierSyntax> Modifiers { get; }
        [NotNull] public TypeKeywordToken TypeKeyword { get; }
        [NotNull] public override IIdentifierToken Name { get; }
        [CanBeNull] public GenericParametersSyntax GenericParameters { get; }
        [NotNull] public IOpenBraceToken OpenBrace { get; }
        [NotNull] public SyntaxList<MemberDeclarationSyntax> Members { get; }
        [NotNull] public ICloseBraceToken CloseBrace { get; }

        public TypeDeclarationSyntax(
            [NotNull] SyntaxList<ModifierSyntax> modifiers,
            [NotNull] TypeKeywordToken typeKeyword,
            [NotNull] IIdentifierToken name,
            [CanBeNull] GenericParametersSyntax genericParameters,
            [NotNull] IOpenBraceToken openBrace,
            [NotNull] SyntaxList<MemberDeclarationSyntax> members,
            [NotNull] ICloseBraceToken closeBrace)
        {
            Requires.NotNull(nameof(modifiers), modifiers);
            Requires.NotNull(nameof(typeKeyword), typeKeyword);
            Requires.NotNull(nameof(name), name);
            Requires.NotNull(nameof(openBrace), openBrace);
            Requires.NotNull(nameof(members), members);
            Requires.NotNull(nameof(closeBrace), closeBrace);
            Modifiers = modifiers;
            TypeKeyword = typeKeyword;
            Name = name;
            GenericParameters = genericParameters;
            OpenBrace = openBrace;
            Members = members;
            CloseBrace = closeBrace;
        }
    }
}