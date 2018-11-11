using System.Collections.Generic;
using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Tokens;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Syntax
{
    public class PlacementInitExpressionSyntax : ExpressionSyntax
    {
        [NotNull] public IInitKeywordTokenPlace InitKeyword { get; }
        [NotNull] public IOpenParenTokenPlace OpenParen { get; }
        [NotNull] public ExpressionSyntax PlaceExpression { get; }
        [NotNull] public ICloseParenTokenPlace CloseParen { get; }
        [NotNull] public NameSyntax Initializer { get; }
        [NotNull] public IOpenParenTokenPlace ArgumentsOpenParen { get; }
        [NotNull] public SeparatedListSyntax<ArgumentSyntax> ArgumentList { get; }
        [NotNull] [ItemNotNull] public IEnumerable<ArgumentSyntax> Arguments => ArgumentList.Nodes();
        [NotNull] public ICloseParenTokenPlace ArgumentsCloseParen { get; }

        public PlacementInitExpressionSyntax(
            [NotNull] IInitKeywordTokenPlace initKeyword,
            [NotNull] IOpenParenTokenPlace openParen,
            [NotNull] ExpressionSyntax placeExpression,
            [NotNull] ICloseParenTokenPlace closeParen,
            [NotNull] NameSyntax initializer,
            [NotNull] IOpenParenTokenPlace argumentsOpenParen,
            [NotNull] SeparatedListSyntax<ArgumentSyntax> argumentList,
            [NotNull] ICloseParenTokenPlace argumentsCloseParen)
            : base(TextSpan.Covering(initKeyword.Span, argumentsCloseParen.Span))
        {
            Requires.NotNull(nameof(initKeyword), initKeyword);
            Requires.NotNull(nameof(openParen), openParen);
            Requires.NotNull(nameof(placeExpression), placeExpression);
            Requires.NotNull(nameof(closeParen), closeParen);
            Requires.NotNull(nameof(initializer), initializer);
            Requires.NotNull(nameof(argumentsOpenParen), argumentsOpenParen);
            Requires.NotNull(nameof(argumentList), argumentList);
            Requires.NotNull(nameof(argumentsCloseParen), argumentsCloseParen);
            InitKeyword = initKeyword;
            OpenParen = openParen;
            PlaceExpression = placeExpression;
            CloseParen = closeParen;
            Initializer = initializer;
            ArgumentsOpenParen = argumentsOpenParen;
            ArgumentList = argumentList;
            ArgumentsCloseParen = argumentsCloseParen;
        }
    }
}