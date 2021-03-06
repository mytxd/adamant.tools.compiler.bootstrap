using System;
using Adamant.Tools.Compiler.Bootstrap.Metadata.Types;

namespace Adamant.Tools.Compiler.Bootstrap.AST
{
    public abstract class StatementSyntax : Syntax
    {
        private DataType type;
        public DataType Type
        {
            get => type;
            set
            {
                if (type != null) throw new InvalidOperationException("Can't set type repeatedly");
                type = value ?? throw new ArgumentException();
            }
        }
    }
}
