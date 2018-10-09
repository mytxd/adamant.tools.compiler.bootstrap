using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Core.Diagnostics;
using Adamant.Tools.Compiler.Bootstrap.Core.Tests;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Tokens;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Syntax.UnitTests.Framework
{
    public class PsuedoToken
    {
        [NotNull]
        public readonly Type TokenType;

        [NotNull]
        public readonly string Text;
        public readonly object Value;

        public PsuedoToken([NotNull] Type tokenType, [NotNull] string text, object value = null)
        {
            TokenType = tokenType;
            Text = text;
            Value = value;
        }

        public static PsuedoToken EndOfFile()
        {
            return new PsuedoToken(typeof(EndOfFileToken), "", new List<Diagnostic>().AsReadOnly());
        }

        public static PsuedoToken For([NotNull] Token token, [NotNull] CodeText code)
        {
            switch (token)
            {
                case IdentifierToken identifier:
                    return new PsuedoToken(token.GetType(), token.Text(code), identifier.Value);
                case StringLiteralToken stringLiteral:
                    return new PsuedoToken(token.GetType(), token.Text(code), stringLiteral.Value);
                case IntegerLiteralToken integerLiteral:
                    return new PsuedoToken(token.GetType(), token.Text(code), integerLiteral.Value);
                case EndOfFileToken eof:
                    return new PsuedoToken(token.GetType(), token.Text(code), eof.Diagnostics);
                case OperatorToken _:
                case KeywordToken _:
                case SymbolToken _:
                case TriviaToken _:
                    return new PsuedoToken(token.GetType(), token.Text(code));
                default:
                    throw NonExhaustiveMatchException.For(token);
            }
        }

        public CodeFile ToFakeCodeFile()
        {
            return Text.ToFakeCodeFile();
        }

        public override bool Equals(object obj)
        {
            if (obj is PsuedoToken token &&
                TokenType == token.TokenType &&
                Text == token.Text)
            {
                if (Value is IReadOnlyList<Diagnostic> diagnostics
                    && token.Value is IReadOnlyList<Diagnostic> otherDiagnostics)
                {
                    return diagnostics.Zip(otherDiagnostics, (d1, d2) => false).All(i => i);
                }
                return EqualityComparer<object>.Default.Equals(Value, token.Value);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TokenType, Text, Value);
        }

        public override string ToString()
        {
            var textValue = string.IsNullOrEmpty(Text) ? "" : $":„{Text}„";
            switch (Value)
            {
                case null:
                    return $"{TokenType}{textValue}";
                case string s:
                    return $"{TokenType}{textValue} 【{Regex.Escape(s)}】";
                case BigInteger i:
                    return $"{TokenType}{textValue} {i}";
                case IReadOnlyList<Diagnostic> diagnostics:
                    return $"{TokenType}{textValue} [{diagnostics.DebugFormat()}]";
                default:
                    return $"{TokenType}{textValue} InvalidValue={Value}";
            }
        }
    }
}