using System;
using System.Collections.Generic;
using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Framework;

namespace Adamant.Tools.Compiler.Bootstrap.Tokens
{
    public static partial class TokenTypes
    {
        private static readonly IReadOnlyList<Type> Keyword = new List<Type>()
        {
            typeof(PublishedKeywordToken),
            typeof(PublicKeywordToken),
            typeof(ProtectedKeywordToken),
            typeof(LetKeywordToken),
            typeof(VarKeywordToken),
            typeof(VoidKeywordToken),
            typeof(IntKeywordToken),
            typeof(Int8KeywordToken),
            typeof(Int16KeywordToken),
            typeof(Int64KeywordToken),
            typeof(UIntKeywordToken),
            typeof(UInt16KeywordToken),
            typeof(UInt64KeywordToken),
            typeof(ByteKeywordToken),
            typeof(SizeKeywordToken),
            typeof(OffsetKeywordToken),
            typeof(BoolKeywordToken),
            typeof(NeverKeywordToken),
            typeof(ReturnKeywordToken),
            typeof(ClassKeywordToken),
            typeof(FunctionKeywordToken),
            typeof(NewKeywordToken),
            typeof(InitKeywordToken),
            typeof(DeleteKeywordToken),
            typeof(OwnedKeywordToken),
            typeof(ForeverKeywordToken),
            typeof(NamespaceKeywordToken),
            typeof(UsingKeywordToken),
            typeof(ForeachKeywordToken),
            typeof(InKeywordToken),
            typeof(IfKeywordToken),
            typeof(ElseKeywordToken),
            typeof(StructKeywordToken),
            typeof(EnumKeywordToken),
            typeof(UnsafeKeywordToken),
            typeof(SafeKeywordToken),
            typeof(SelfKeywordToken),
            typeof(SelfTypeKeywordToken),
            typeof(BaseKeywordToken),
            typeof(TypeKeywordToken),
            typeof(MutableKeywordToken),
            typeof(ParamsKeywordToken),
            typeof(MayKeywordToken),
            typeof(NoKeywordToken),
            typeof(ThrowKeywordToken),
            typeof(RefKeywordToken),
            typeof(AbstractKeywordToken),
            typeof(GetKeywordToken),
            typeof(SetKeywordToken),
            typeof(RequiresKeywordToken),
            typeof(EnsuresKeywordToken),
            typeof(InvariantKeywordToken),
            typeof(WhereKeywordToken),
            typeof(ConstKeywordToken),
            typeof(UninitializedKeywordToken),
            typeof(NoneKeywordToken),
            typeof(OperatorKeywordToken),
            typeof(ImplicitKeywordToken),
            typeof(ExplicitKeywordToken),
            typeof(MoveKeywordToken),
            typeof(CopyKeywordToken),
            typeof(MatchKeywordToken),
            typeof(LoopKeywordToken),
            typeof(WhileKeywordToken),
            typeof(BreakKeywordToken),
            typeof(NextKeywordToken),
            typeof(OverrideKeywordToken),
            typeof(AnyKeywordToken),
            typeof(TrueKeywordToken),
            typeof(FalseKeywordToken),
            typeof(AsKeywordToken),
            typeof(AndKeywordToken),
            typeof(OrKeywordToken),
            typeof(NotKeywordToken),
            typeof(TraitKeywordToken),
            typeof(FloatKeywordToken),
            typeof(Float32KeywordToken),
            typeof(UnderscoreKeywordToken),
            typeof(ExternalKeywordToken),
        }.AsReadOnly();
    }

    public static partial class TokenFactory
    {
        public static IPublishedKeywordToken PublishedKeyword(TextSpan span)
        {
            return new PublishedKeywordToken(span);
        }
        public static IPublicKeywordToken PublicKeyword(TextSpan span)
        {
            return new PublicKeywordToken(span);
        }
        public static IProtectedKeywordToken ProtectedKeyword(TextSpan span)
        {
            return new ProtectedKeywordToken(span);
        }
        public static ILetKeywordToken LetKeyword(TextSpan span)
        {
            return new LetKeywordToken(span);
        }
        public static IVarKeywordToken VarKeyword(TextSpan span)
        {
            return new VarKeywordToken(span);
        }
        public static IVoidKeywordToken VoidKeyword(TextSpan span)
        {
            return new VoidKeywordToken(span);
        }
        public static IIntKeywordToken IntKeyword(TextSpan span)
        {
            return new IntKeywordToken(span);
        }
        public static IInt8KeywordToken Int8Keyword(TextSpan span)
        {
            return new Int8KeywordToken(span);
        }
        public static IInt16KeywordToken Int16Keyword(TextSpan span)
        {
            return new Int16KeywordToken(span);
        }
        public static IInt64KeywordToken Int64Keyword(TextSpan span)
        {
            return new Int64KeywordToken(span);
        }
        public static IUIntKeywordToken UIntKeyword(TextSpan span)
        {
            return new UIntKeywordToken(span);
        }
        public static IUInt16KeywordToken UInt16Keyword(TextSpan span)
        {
            return new UInt16KeywordToken(span);
        }
        public static IUInt64KeywordToken UInt64Keyword(TextSpan span)
        {
            return new UInt64KeywordToken(span);
        }
        public static IByteKeywordToken ByteKeyword(TextSpan span)
        {
            return new ByteKeywordToken(span);
        }
        public static ISizeKeywordToken SizeKeyword(TextSpan span)
        {
            return new SizeKeywordToken(span);
        }
        public static IOffsetKeywordToken OffsetKeyword(TextSpan span)
        {
            return new OffsetKeywordToken(span);
        }
        public static IBoolKeywordToken BoolKeyword(TextSpan span)
        {
            return new BoolKeywordToken(span);
        }
        public static INeverKeywordToken NeverKeyword(TextSpan span)
        {
            return new NeverKeywordToken(span);
        }
        public static IReturnKeywordToken ReturnKeyword(TextSpan span)
        {
            return new ReturnKeywordToken(span);
        }
        public static IClassKeywordToken ClassKeyword(TextSpan span)
        {
            return new ClassKeywordToken(span);
        }
        public static IFunctionKeywordToken FunctionKeyword(TextSpan span)
        {
            return new FunctionKeywordToken(span);
        }
        public static INewKeywordToken NewKeyword(TextSpan span)
        {
            return new NewKeywordToken(span);
        }
        public static IInitKeywordToken InitKeyword(TextSpan span)
        {
            return new InitKeywordToken(span);
        }
        public static IDeleteKeywordToken DeleteKeyword(TextSpan span)
        {
            return new DeleteKeywordToken(span);
        }
        public static IOwnedKeywordToken OwnedKeyword(TextSpan span)
        {
            return new OwnedKeywordToken(span);
        }
        public static IForeverKeywordToken ForeverKeyword(TextSpan span)
        {
            return new ForeverKeywordToken(span);
        }
        public static INamespaceKeywordToken NamespaceKeyword(TextSpan span)
        {
            return new NamespaceKeywordToken(span);
        }
        public static IUsingKeywordToken UsingKeyword(TextSpan span)
        {
            return new UsingKeywordToken(span);
        }
        public static IForeachKeywordToken ForeachKeyword(TextSpan span)
        {
            return new ForeachKeywordToken(span);
        }
        public static IInKeywordToken InKeyword(TextSpan span)
        {
            return new InKeywordToken(span);
        }
        public static IIfKeywordToken IfKeyword(TextSpan span)
        {
            return new IfKeywordToken(span);
        }
        public static IElseKeywordToken ElseKeyword(TextSpan span)
        {
            return new ElseKeywordToken(span);
        }
        public static IStructKeywordToken StructKeyword(TextSpan span)
        {
            return new StructKeywordToken(span);
        }
        public static IEnumKeywordToken EnumKeyword(TextSpan span)
        {
            return new EnumKeywordToken(span);
        }
        public static IUnsafeKeywordToken UnsafeKeyword(TextSpan span)
        {
            return new UnsafeKeywordToken(span);
        }
        public static ISafeKeywordToken SafeKeyword(TextSpan span)
        {
            return new SafeKeywordToken(span);
        }
        public static ISelfKeywordToken SelfKeyword(TextSpan span)
        {
            return new SelfKeywordToken(span);
        }
        public static ISelfTypeKeywordToken SelfTypeKeyword(TextSpan span)
        {
            return new SelfTypeKeywordToken(span);
        }
        public static IBaseKeywordToken BaseKeyword(TextSpan span)
        {
            return new BaseKeywordToken(span);
        }
        public static ITypeKeywordToken TypeKeyword(TextSpan span)
        {
            return new TypeKeywordToken(span);
        }
        public static IMutableKeywordToken MutableKeyword(TextSpan span)
        {
            return new MutableKeywordToken(span);
        }
        public static IParamsKeywordToken ParamsKeyword(TextSpan span)
        {
            return new ParamsKeywordToken(span);
        }
        public static IMayKeywordToken MayKeyword(TextSpan span)
        {
            return new MayKeywordToken(span);
        }
        public static INoKeywordToken NoKeyword(TextSpan span)
        {
            return new NoKeywordToken(span);
        }
        public static IThrowKeywordToken ThrowKeyword(TextSpan span)
        {
            return new ThrowKeywordToken(span);
        }
        public static IRefKeywordToken RefKeyword(TextSpan span)
        {
            return new RefKeywordToken(span);
        }
        public static IAbstractKeywordToken AbstractKeyword(TextSpan span)
        {
            return new AbstractKeywordToken(span);
        }
        public static IGetKeywordToken GetKeyword(TextSpan span)
        {
            return new GetKeywordToken(span);
        }
        public static ISetKeywordToken SetKeyword(TextSpan span)
        {
            return new SetKeywordToken(span);
        }
        public static IRequiresKeywordToken RequiresKeyword(TextSpan span)
        {
            return new RequiresKeywordToken(span);
        }
        public static IEnsuresKeywordToken EnsuresKeyword(TextSpan span)
        {
            return new EnsuresKeywordToken(span);
        }
        public static IInvariantKeywordToken InvariantKeyword(TextSpan span)
        {
            return new InvariantKeywordToken(span);
        }
        public static IWhereKeywordToken WhereKeyword(TextSpan span)
        {
            return new WhereKeywordToken(span);
        }
        public static IConstKeywordToken ConstKeyword(TextSpan span)
        {
            return new ConstKeywordToken(span);
        }
        public static IUninitializedKeywordToken UninitializedKeyword(TextSpan span)
        {
            return new UninitializedKeywordToken(span);
        }
        public static INoneKeywordToken NoneKeyword(TextSpan span)
        {
            return new NoneKeywordToken(span);
        }
        public static IOperatorKeywordToken OperatorKeyword(TextSpan span)
        {
            return new OperatorKeywordToken(span);
        }
        public static IImplicitKeywordToken ImplicitKeyword(TextSpan span)
        {
            return new ImplicitKeywordToken(span);
        }
        public static IExplicitKeywordToken ExplicitKeyword(TextSpan span)
        {
            return new ExplicitKeywordToken(span);
        }
        public static IMoveKeywordToken MoveKeyword(TextSpan span)
        {
            return new MoveKeywordToken(span);
        }
        public static ICopyKeywordToken CopyKeyword(TextSpan span)
        {
            return new CopyKeywordToken(span);
        }
        public static IMatchKeywordToken MatchKeyword(TextSpan span)
        {
            return new MatchKeywordToken(span);
        }
        public static ILoopKeywordToken LoopKeyword(TextSpan span)
        {
            return new LoopKeywordToken(span);
        }
        public static IWhileKeywordToken WhileKeyword(TextSpan span)
        {
            return new WhileKeywordToken(span);
        }
        public static IBreakKeywordToken BreakKeyword(TextSpan span)
        {
            return new BreakKeywordToken(span);
        }
        public static INextKeywordToken NextKeyword(TextSpan span)
        {
            return new NextKeywordToken(span);
        }
        public static IOverrideKeywordToken OverrideKeyword(TextSpan span)
        {
            return new OverrideKeywordToken(span);
        }
        public static IAnyKeywordToken AnyKeyword(TextSpan span)
        {
            return new AnyKeywordToken(span);
        }
        public static ITrueKeywordToken TrueKeyword(TextSpan span)
        {
            return new TrueKeywordToken(span);
        }
        public static IFalseKeywordToken FalseKeyword(TextSpan span)
        {
            return new FalseKeywordToken(span);
        }
        public static IAsKeywordToken AsKeyword(TextSpan span)
        {
            return new AsKeywordToken(span);
        }
        public static IAndKeywordToken AndKeyword(TextSpan span)
        {
            return new AndKeywordToken(span);
        }
        public static IOrKeywordToken OrKeyword(TextSpan span)
        {
            return new OrKeywordToken(span);
        }
        public static INotKeywordToken NotKeyword(TextSpan span)
        {
            return new NotKeywordToken(span);
        }
        public static ITraitKeywordToken TraitKeyword(TextSpan span)
        {
            return new TraitKeywordToken(span);
        }
        public static IFloatKeywordToken FloatKeyword(TextSpan span)
        {
            return new FloatKeywordToken(span);
        }
        public static IFloat32KeywordToken Float32Keyword(TextSpan span)
        {
            return new Float32KeywordToken(span);
        }
        public static IUnderscoreKeywordToken UnderscoreKeyword(TextSpan span)
        {
            return new UnderscoreKeywordToken(span);
        }
        public static IExternalKeywordToken ExternalKeyword(TextSpan span)
        {
            return new ExternalKeywordToken(span);
        }
    }

    public partial interface IPublishedKeywordToken : IKeywordToken { }
    internal partial class PublishedKeywordToken : Token, IPublishedKeywordToken
    {
        public PublishedKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IPublicKeywordToken : IKeywordToken { }
    internal partial class PublicKeywordToken : Token, IPublicKeywordToken
    {
        public PublicKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IProtectedKeywordToken : IKeywordToken { }
    internal partial class ProtectedKeywordToken : Token, IProtectedKeywordToken
    {
        public ProtectedKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface ILetKeywordToken : IKeywordToken { }
    internal partial class LetKeywordToken : Token, ILetKeywordToken
    {
        public LetKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IVarKeywordToken : IKeywordToken { }
    internal partial class VarKeywordToken : Token, IVarKeywordToken
    {
        public VarKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IVoidKeywordToken : IKeywordToken { }
    internal partial class VoidKeywordToken : Token, IVoidKeywordToken
    {
        public VoidKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IIntKeywordToken : IKeywordToken { }
    internal partial class IntKeywordToken : Token, IIntKeywordToken
    {
        public IntKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IInt8KeywordToken : IKeywordToken { }
    internal partial class Int8KeywordToken : Token, IInt8KeywordToken
    {
        public Int8KeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IInt16KeywordToken : IKeywordToken { }
    internal partial class Int16KeywordToken : Token, IInt16KeywordToken
    {
        public Int16KeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IInt64KeywordToken : IKeywordToken { }
    internal partial class Int64KeywordToken : Token, IInt64KeywordToken
    {
        public Int64KeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IUIntKeywordToken : IKeywordToken { }
    internal partial class UIntKeywordToken : Token, IUIntKeywordToken
    {
        public UIntKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IUInt16KeywordToken : IKeywordToken { }
    internal partial class UInt16KeywordToken : Token, IUInt16KeywordToken
    {
        public UInt16KeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IUInt64KeywordToken : IKeywordToken { }
    internal partial class UInt64KeywordToken : Token, IUInt64KeywordToken
    {
        public UInt64KeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IByteKeywordToken : IKeywordToken { }
    internal partial class ByteKeywordToken : Token, IByteKeywordToken
    {
        public ByteKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface ISizeKeywordToken : IKeywordToken { }
    internal partial class SizeKeywordToken : Token, ISizeKeywordToken
    {
        public SizeKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IOffsetKeywordToken : IKeywordToken { }
    internal partial class OffsetKeywordToken : Token, IOffsetKeywordToken
    {
        public OffsetKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IBoolKeywordToken : IKeywordToken { }
    internal partial class BoolKeywordToken : Token, IBoolKeywordToken
    {
        public BoolKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface INeverKeywordToken : IKeywordToken { }
    internal partial class NeverKeywordToken : Token, INeverKeywordToken
    {
        public NeverKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IReturnKeywordToken : IKeywordToken { }
    internal partial class ReturnKeywordToken : Token, IReturnKeywordToken
    {
        public ReturnKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IClassKeywordToken : IKeywordToken { }
    internal partial class ClassKeywordToken : Token, IClassKeywordToken
    {
        public ClassKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IFunctionKeywordToken : IKeywordToken { }
    internal partial class FunctionKeywordToken : Token, IFunctionKeywordToken
    {
        public FunctionKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface INewKeywordToken : IKeywordToken { }
    internal partial class NewKeywordToken : Token, INewKeywordToken
    {
        public NewKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IInitKeywordToken : IKeywordToken { }
    internal partial class InitKeywordToken : Token, IInitKeywordToken
    {
        public InitKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IDeleteKeywordToken : IKeywordToken { }
    internal partial class DeleteKeywordToken : Token, IDeleteKeywordToken
    {
        public DeleteKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IOwnedKeywordToken : IKeywordToken { }
    internal partial class OwnedKeywordToken : Token, IOwnedKeywordToken
    {
        public OwnedKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IForeverKeywordToken : IKeywordToken { }
    internal partial class ForeverKeywordToken : Token, IForeverKeywordToken
    {
        public ForeverKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface INamespaceKeywordToken : IKeywordToken { }
    internal partial class NamespaceKeywordToken : Token, INamespaceKeywordToken
    {
        public NamespaceKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IUsingKeywordToken : IKeywordToken { }
    internal partial class UsingKeywordToken : Token, IUsingKeywordToken
    {
        public UsingKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IForeachKeywordToken : IKeywordToken { }
    internal partial class ForeachKeywordToken : Token, IForeachKeywordToken
    {
        public ForeachKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IInKeywordToken : IKeywordToken { }
    internal partial class InKeywordToken : Token, IInKeywordToken
    {
        public InKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IIfKeywordToken : IKeywordToken { }
    internal partial class IfKeywordToken : Token, IIfKeywordToken
    {
        public IfKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IElseKeywordToken : IKeywordToken { }
    internal partial class ElseKeywordToken : Token, IElseKeywordToken
    {
        public ElseKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IStructKeywordToken : IKeywordToken { }
    internal partial class StructKeywordToken : Token, IStructKeywordToken
    {
        public StructKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IEnumKeywordToken : IKeywordToken { }
    internal partial class EnumKeywordToken : Token, IEnumKeywordToken
    {
        public EnumKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IUnsafeKeywordToken : IKeywordToken { }
    internal partial class UnsafeKeywordToken : Token, IUnsafeKeywordToken
    {
        public UnsafeKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface ISafeKeywordToken : IKeywordToken { }
    internal partial class SafeKeywordToken : Token, ISafeKeywordToken
    {
        public SafeKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface ISelfKeywordToken : IKeywordToken { }
    internal partial class SelfKeywordToken : Token, ISelfKeywordToken
    {
        public SelfKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface ISelfTypeKeywordToken : IKeywordToken { }
    internal partial class SelfTypeKeywordToken : Token, ISelfTypeKeywordToken
    {
        public SelfTypeKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IBaseKeywordToken : IKeywordToken { }
    internal partial class BaseKeywordToken : Token, IBaseKeywordToken
    {
        public BaseKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface ITypeKeywordToken : IKeywordToken { }
    internal partial class TypeKeywordToken : Token, ITypeKeywordToken
    {
        public TypeKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IMutableKeywordToken : IKeywordToken { }
    internal partial class MutableKeywordToken : Token, IMutableKeywordToken
    {
        public MutableKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IParamsKeywordToken : IKeywordToken { }
    internal partial class ParamsKeywordToken : Token, IParamsKeywordToken
    {
        public ParamsKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IMayKeywordToken : IKeywordToken { }
    internal partial class MayKeywordToken : Token, IMayKeywordToken
    {
        public MayKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface INoKeywordToken : IKeywordToken { }
    internal partial class NoKeywordToken : Token, INoKeywordToken
    {
        public NoKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IThrowKeywordToken : IKeywordToken { }
    internal partial class ThrowKeywordToken : Token, IThrowKeywordToken
    {
        public ThrowKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IRefKeywordToken : IKeywordToken { }
    internal partial class RefKeywordToken : Token, IRefKeywordToken
    {
        public RefKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IAbstractKeywordToken : IKeywordToken { }
    internal partial class AbstractKeywordToken : Token, IAbstractKeywordToken
    {
        public AbstractKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IGetKeywordToken : IKeywordToken { }
    internal partial class GetKeywordToken : Token, IGetKeywordToken
    {
        public GetKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface ISetKeywordToken : IKeywordToken { }
    internal partial class SetKeywordToken : Token, ISetKeywordToken
    {
        public SetKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IRequiresKeywordToken : IKeywordToken { }
    internal partial class RequiresKeywordToken : Token, IRequiresKeywordToken
    {
        public RequiresKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IEnsuresKeywordToken : IKeywordToken { }
    internal partial class EnsuresKeywordToken : Token, IEnsuresKeywordToken
    {
        public EnsuresKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IInvariantKeywordToken : IKeywordToken { }
    internal partial class InvariantKeywordToken : Token, IInvariantKeywordToken
    {
        public InvariantKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IWhereKeywordToken : IKeywordToken { }
    internal partial class WhereKeywordToken : Token, IWhereKeywordToken
    {
        public WhereKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IConstKeywordToken : IKeywordToken { }
    internal partial class ConstKeywordToken : Token, IConstKeywordToken
    {
        public ConstKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IUninitializedKeywordToken : IKeywordToken { }
    internal partial class UninitializedKeywordToken : Token, IUninitializedKeywordToken
    {
        public UninitializedKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface INoneKeywordToken : IKeywordToken { }
    internal partial class NoneKeywordToken : Token, INoneKeywordToken
    {
        public NoneKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IOperatorKeywordToken : IKeywordToken { }
    internal partial class OperatorKeywordToken : Token, IOperatorKeywordToken
    {
        public OperatorKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IImplicitKeywordToken : IKeywordToken { }
    internal partial class ImplicitKeywordToken : Token, IImplicitKeywordToken
    {
        public ImplicitKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IExplicitKeywordToken : IKeywordToken { }
    internal partial class ExplicitKeywordToken : Token, IExplicitKeywordToken
    {
        public ExplicitKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IMoveKeywordToken : IKeywordToken { }
    internal partial class MoveKeywordToken : Token, IMoveKeywordToken
    {
        public MoveKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface ICopyKeywordToken : IKeywordToken { }
    internal partial class CopyKeywordToken : Token, ICopyKeywordToken
    {
        public CopyKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IMatchKeywordToken : IKeywordToken { }
    internal partial class MatchKeywordToken : Token, IMatchKeywordToken
    {
        public MatchKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface ILoopKeywordToken : IKeywordToken { }
    internal partial class LoopKeywordToken : Token, ILoopKeywordToken
    {
        public LoopKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IWhileKeywordToken : IKeywordToken { }
    internal partial class WhileKeywordToken : Token, IWhileKeywordToken
    {
        public WhileKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IBreakKeywordToken : IKeywordToken { }
    internal partial class BreakKeywordToken : Token, IBreakKeywordToken
    {
        public BreakKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface INextKeywordToken : IKeywordToken { }
    internal partial class NextKeywordToken : Token, INextKeywordToken
    {
        public NextKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IOverrideKeywordToken : IKeywordToken { }
    internal partial class OverrideKeywordToken : Token, IOverrideKeywordToken
    {
        public OverrideKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IAnyKeywordToken : IKeywordToken { }
    internal partial class AnyKeywordToken : Token, IAnyKeywordToken
    {
        public AnyKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface ITrueKeywordToken : IKeywordToken { }
    internal partial class TrueKeywordToken : Token, ITrueKeywordToken
    {
        public TrueKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IFalseKeywordToken : IKeywordToken { }
    internal partial class FalseKeywordToken : Token, IFalseKeywordToken
    {
        public FalseKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IAsKeywordToken : IKeywordToken { }
    internal partial class AsKeywordToken : Token, IAsKeywordToken
    {
        public AsKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IAndKeywordToken : IKeywordToken { }
    internal partial class AndKeywordToken : Token, IAndKeywordToken
    {
        public AndKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IOrKeywordToken : IKeywordToken { }
    internal partial class OrKeywordToken : Token, IOrKeywordToken
    {
        public OrKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface INotKeywordToken : IKeywordToken { }
    internal partial class NotKeywordToken : Token, INotKeywordToken
    {
        public NotKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface ITraitKeywordToken : IKeywordToken { }
    internal partial class TraitKeywordToken : Token, ITraitKeywordToken
    {
        public TraitKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IFloatKeywordToken : IKeywordToken { }
    internal partial class FloatKeywordToken : Token, IFloatKeywordToken
    {
        public FloatKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IFloat32KeywordToken : IKeywordToken { }
    internal partial class Float32KeywordToken : Token, IFloat32KeywordToken
    {
        public Float32KeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IUnderscoreKeywordToken : IKeywordToken { }
    internal partial class UnderscoreKeywordToken : Token, IUnderscoreKeywordToken
    {
        public UnderscoreKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }

    public partial interface IExternalKeywordToken : IKeywordToken { }
    internal partial class ExternalKeywordToken : Token, IExternalKeywordToken
    {
        public ExternalKeywordToken(TextSpan span)
            : base(span)
        {
        }
    }
}
