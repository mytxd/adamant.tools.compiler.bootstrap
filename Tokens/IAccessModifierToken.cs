namespace Adamant.Tools.Compiler.Bootstrap.Tokens
{
    public partial interface IAccessModifierToken : IKeywordToken { }

    public partial interface IPublicKeywordToken : IAccessModifierToken { }
    public partial interface IProtectedKeywordToken : IAccessModifierToken { }
    public partial interface IPrivateKeywordToken : IAccessModifierToken { }
    public partial interface IInternalKeywordToken : IAccessModifierToken { }
}