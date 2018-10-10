using System.Collections.Generic;
using System.Linq;
using Adamant.Tools.Compiler.Bootstrap.Core.Diagnostics;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Types;
using Adamant.Tools.Compiler.Bootstrap.Syntax;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Declarations;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics.Nodes.Declarations
{
    public class EnumStructDeclaration : MemberDeclaration
    {
        public new EnumStructDeclarationSyntax Syntax { get; }
        public AccessModifier Access { get; }
        public bool IsMutable { get; }
        [NotNull] public string Name { get; }
        public ObjectType Type { get; }
        public IReadOnlyList<MemberDeclaration> Members { get; }

        public EnumStructDeclaration(
            EnumStructDeclarationSyntax syntax,
            IEnumerable<Diagnostic> diagnostics,
            AccessModifier access,
            ObjectType type,
            IEnumerable<MemberDeclaration> members)
            : base(diagnostics)
        {
            Syntax = syntax;
            Access = access;
            IsMutable = type.IsMutable;
            Name = type.Name.EntityName;
            Type = type;
            Members = members.ToList().AsReadOnly();
        }

        protected override SyntaxNode GetSyntax() => Syntax;

        public override void AllDiagnostics(List<Diagnostic> list)
        {
            base.AllDiagnostics(list);
            foreach (var member in Members)
                member.AllDiagnostics(list);
        }
    }
}