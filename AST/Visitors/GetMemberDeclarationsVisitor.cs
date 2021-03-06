using System.Collections.Generic;
using Adamant.Tools.Compiler.Bootstrap.Framework;

namespace Adamant.Tools.Compiler.Bootstrap.AST.Visitors
{
    public class GetMemberDeclarationsVisitor : DeclarationVisitor<Void>
    {
        public IReadOnlyList<MemberDeclarationSyntax> MemberDeclarations => memberDeclarations;
        private readonly List<MemberDeclarationSyntax> memberDeclarations = new List<MemberDeclarationSyntax>();

        public override void VisitMemberDeclaration(MemberDeclarationSyntax memberDeclaration, Void args)
        {
            base.VisitMemberDeclaration(memberDeclaration, args);
            memberDeclarations.Add(memberDeclaration);
        }

        public override void VisitFunctionDeclaration(FunctionDeclarationSyntax functionDeclaration, Void args)
        {
            // No need to visit the children, no members there
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax constructorDeclaration, Void args)
        {
            // No need to visit the children, no members there
        }

        public override void VisitExpression(ExpressionSyntax expression, Void args)
        {
            // We aren't supposed to be trying to go into expressions
            throw new System.InvalidOperationException();
        }
    }
}
