using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Tokens;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Syntax
{
    public class QualifiedNameSyntax : NameSyntax
    {
        [NotNull] public NameSyntax Qualifier { get; }
        [NotNull] public IDotToken Dot { get; }
        [NotNull] public SimpleNameSyntax Name { get; }

        public QualifiedNameSyntax(
            [NotNull] NameSyntax qualifier,
            [NotNull] IDotToken dot,
            [NotNull] SimpleNameSyntax name)
            : base(TextSpan.Covering(qualifier.Span, name.Span))
        {
            Requires.NotNull(nameof(qualifier), qualifier);
            Requires.NotNull(nameof(dot), dot);
            Requires.NotNull(nameof(name), name);
            Qualifier = qualifier;
            Dot = dot;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Qualifier}.{Name}";
        }
    }
}
