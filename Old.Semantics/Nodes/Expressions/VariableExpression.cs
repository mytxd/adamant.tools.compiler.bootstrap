using System.Collections.Generic;
using Adamant.Tools.Compiler.Bootstrap.Core.Diagnostics;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Names;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Types;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Expressions.Types.Names;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics.Nodes.Expressions
{
    public class VariableExpression : Expression
    {
        public new IdentifierNameSyntax Syntax { get; }
        public string Name => Syntax.Name.Value;
        public VariableName VariableName { get; }

        public VariableExpression(
            IdentifierNameSyntax syntax,
            IEnumerable<Diagnostic> diagnostics,
            VariableName variableName,
            DataType type)
            : base(diagnostics, type)
        {
            Syntax = syntax;
            VariableName = variableName;
        }

        protected override SyntaxNode GetSyntax()
        {
            return Syntax;
        }
    }
}