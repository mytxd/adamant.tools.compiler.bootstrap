using Adamant.Tools.Compiler.Bootstrap.AST;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Names;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Scopes
{
    public class NamespaceScope : NestedScope
    {
        [NotNull] public new NamespaceDeclarationSyntax Syntax { get; }
        [NotNull] public Name Name { get; }

        public NamespaceScope(
            [NotNull] LexicalScope containingScope,
            [NotNull] NamespaceDeclarationSyntax @namespace,
            [NotNull] Name name)
            : base(containingScope, @namespace)
        {
            Requires.NotNull(nameof(name), name);
            Syntax = @namespace;
            Name = name;
        }
    }
}