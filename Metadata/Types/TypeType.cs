namespace Adamant.Tools.Compiler.Bootstrap.Metadata.Types
{
    public class TypeType : ReferenceType
    {
        #region Singleton
        internal static readonly TypeType Instance = new TypeType();

        private TypeType()
            : base(Lifetimes.Lifetime.Forever)
        { }
        #endregion

        public override bool IsResolved => true;

        public override string ToString() => "Type";
    }
}
