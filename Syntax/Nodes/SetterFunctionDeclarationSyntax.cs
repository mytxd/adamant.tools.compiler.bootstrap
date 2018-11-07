using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Tokens;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes
{
    public class SetterFunctionDeclarationSyntax : FunctionDeclarationSyntax
    {
        [NotNull] public SetKeywordToken SetKeyword { get; }
        [NotNull] public override IIdentifierToken Name { get; }
        [NotNull] public IRightArrowToken Arrow { get; }
        [NotNull] public ExpressionSyntax ReturnTypeExpression { get; }

        public SetterFunctionDeclarationSyntax(
            [NotNull] SyntaxList<ModifierSyntax> modifiers,
            [NotNull] SetKeywordToken setKeyword,
            [NotNull] IIdentifierToken name,
            [NotNull] IOpenParenToken openParen,
            [NotNull] SeparatedListSyntax<ParameterSyntax> parameterList,
            [NotNull] ICloseParenToken closeParen,
            [NotNull] IRightArrowToken arrow,
            [NotNull] ExpressionSyntax returnTypeExpression,
            [CanBeNull] EffectsSyntax effects,
            [NotNull] SyntaxList<FunctionContractSyntax> contracts,
            [CanBeNull] BlockSyntax body,
            [CanBeNull] ISemicolonToken semicolon)
            : base(TextSpan.Covering(setKeyword.Span, name.Span), modifiers, openParen,
                parameterList, closeParen, effects, contracts, body, semicolon)
        {
            Requires.NotNull(nameof(modifiers), modifiers);
            Requires.NotNull(nameof(setKeyword), setKeyword);
            Requires.NotNull(nameof(name), name);
            Requires.NotNull(nameof(openParen), openParen);
            Requires.NotNull(nameof(parameterList), parameterList);
            Requires.NotNull(nameof(closeParen), closeParen);
            Requires.NotNull(nameof(arrow), arrow);
            Requires.NotNull(nameof(returnTypeExpression), returnTypeExpression);
            SetKeyword = setKeyword;
            Name = name;
            Arrow = arrow;
            ReturnTypeExpression = returnTypeExpression;
        }
    }
}