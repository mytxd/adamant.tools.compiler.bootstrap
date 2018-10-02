using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Names;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Nodes;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Nodes.Declarations;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Nodes.Expressions;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Nodes.Expressions.ControlFlow;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Nodes.Expressions.Operators;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Nodes.Statements;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Declarations;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Expressions;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Expressions.ControlFlow;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Expressions.Operators;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Expressions.Types;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Expressions.Types.Names;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes.Statements;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Tokens;
using Core.Syntax;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics
{
    internal partial class SemanticAnalysis
    {
        private const string NodeAttribute = "Node";

        public Package Package => Node<Package>(PackageSyntax);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Package Node(PackageSyntax s) => Node<Package>(s);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CompilationUnit Node(CompilationUnitSyntax s) => Node<CompilationUnit>(s);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CompilationUnitNamespaceDeclaration Node(CompilationUnitNamespaceSyntax s) => Node<CompilationUnitNamespaceDeclaration>(s);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Declaration Node(DeclarationSyntax s) => Node<Declaration>(s);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MemberDeclaration Node(MemberDeclarationSyntax s) => Node<MemberDeclaration>(s);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public FunctionDeclaration Node(FunctionDeclarationSyntax s) => Node<FunctionDeclaration>(s);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Parameter Node(ParameterSyntax s) => Node<Parameter>(s);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Block Node(BlockSyntax s) => Node<Block>(s);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Statement Node(StatementSyntax s) => Node<Statement>(s);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expression Node(ExpressionSyntax s) => Node<Expression>(s);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expression Node(TypeSyntax s) => Node<Expression>(s);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private TNode Node<TNode>(SyntaxBranchNode syntax)
            where TNode : SemanticNode
        {
            if (syntax == null) return null;
            return (TNode)attributes.GetOrAdd(syntax, NodeAttribute, ComputeNode);
        }

        private SemanticNode ComputeNode(SyntaxBranchNode syntax)
        {
            switch (syntax)
            {
                case PackageSyntax package:
                    // To avoid potential contention, we build the syntax symbols
                    // before trying to create all the compilation unit nodes
                    // in parallel.
                    //var syntaxSymbol = SyntaxSymbol.Package;
                    var compilationUnits = package.CompilationUnits
#if RELEASE
                        .AsParallel()
#endif
                        .Select(Node)
                        .ToList();
                    return new Package(package, AllDiagnostics(package), compilationUnits, EntryPoint(package));

                case CompilationUnitSyntax compilationUnit:
                    {
                        var @namespace = compilationUnit.Namespace != null
                            ? Node(compilationUnit.Namespace)
                            : null;
                        return new CompilationUnit(compilationUnit, AllDiagnostics(compilationUnit),
                            @namespace, compilationUnit.Declarations.Select(Node));
                    }
                case CompilationUnitNamespaceSyntax @namespace:
                    return new CompilationUnitNamespaceDeclaration(@namespace, AllDiagnostics(@namespace));

                case FunctionDeclarationSyntax function:
                    return new FunctionDeclaration(
                        function,
                        AllDiagnostics(function),
                        AccessLevel(function.AccessModifier),
                        function.Name.Text,
                        function.Parameters.Select(Node),
                        Node(function.ReturnType),
                        Node(function.Body)); // TODO change this to a list of statements

                case ParameterSyntax parameter:
                    return new Parameter(parameter, AllDiagnostics(parameter),
                        parameter.VarKeyword != null, parameter.Name.Value, Node(parameter.Type));

                case BlockSyntax block:
                    var statements = block.Statements.Select(Node);
                    return new Block(block, AllDiagnostics(block), statements);

                case ExpressionStatementSyntax expressionStatement:
                    return new ExpressionStatement(expressionStatement,
                        AllDiagnostics(expressionStatement), Node(expressionStatement.Expression));

                case IdentifierNameSyntax identifierName:
                    var name = Name(identifierName);
                    switch (name)
                    {
                        case VariableName variableName:
                            return new VariableExpression(identifierName, AllDiagnostics(identifierName),
                                variableName, Type(identifierName));
                        case ReferenceTypeName typeName:
                            return new TypeNameExpression(identifierName, AllDiagnostics(identifierName), Type(identifierName));
                        case UnknownName _:
                            return new UnknownExpression(identifierName, AllDiagnostics(identifierName));
                        default:
                            throw NonExhaustiveMatchException.For(name);
                    }

                case ReturnExpressionSyntax returnExpression:
                    return new ReturnExpression(returnExpression, AllDiagnostics(returnExpression),
                        Node(returnExpression.Expression));

                case BinaryOperatorExpressionSyntax binaryOperatorExpression:
                    {
                        var leftOperand = Node(binaryOperatorExpression.LeftOperand);
                        var rightOperand = Node(binaryOperatorExpression.RightOperand);
                        switch (binaryOperatorExpression.Operator.Kind)
                        {
                            case TokenKind.Plus:
                                return new AddExpression(binaryOperatorExpression, AllDiagnostics(binaryOperatorExpression), leftOperand, rightOperand, Type(binaryOperatorExpression));

                            case TokenKind.Equals:
                                return new AssignmentExpression(binaryOperatorExpression,
                                    AllDiagnostics(binaryOperatorExpression), leftOperand,
                                    rightOperand, Type(binaryOperatorExpression));

                            default:
                                throw new InvalidEnumArgumentException(binaryOperatorExpression.Operator.Kind.ToString());
                        }
                    }

                case TypeSyntax type:
                    return new TypeNameExpression(type, AllDiagnostics(type), Type(type));

                case VariableDeclarationStatementSyntax variableDeclaration:
                    return new VariableDeclarationStatement(variableDeclaration,
                        AllDiagnostics(variableDeclaration),
                        variableDeclaration.Binding.Kind == TokenKind.VarKeyword,
                        variableDeclaration.Name.Value,
                        Type(variableDeclaration),
                        Node(variableDeclaration.Initializer));

                case NewObjectExpressionSyntax newObject:
                    return new NewObjectExpression(newObject,
                        AllDiagnostics(newObject),
                        Type(newObject),
                        newObject.Arguments.Select(Node));

                case ClassDeclarationSyntax classDeclaration:
                    return new ClassDeclaration(
                        classDeclaration,
                        AllDiagnostics(classDeclaration),
                        AccessLevel(classDeclaration.AccessModifier),
                        Type(classDeclaration),
                        classDeclaration.Members.Select(Node));

                case EnumStructDeclarationSyntax enumDeclaration:
                    return new EnumStructDeclaration(
                        enumDeclaration,
                        AllDiagnostics(enumDeclaration),
                        AccessLevel(enumDeclaration.AccessModifier),
                        Type(enumDeclaration),
                        enumDeclaration.Members.Select(Node));

                case IncompleteDeclarationSyntax incompleteDeclaration:
                    return new IncompleteDeclaration(incompleteDeclaration, AllDiagnostics(incompleteDeclaration));

                default:
                    throw NonExhaustiveMatchException.For(syntax);
            }
        }

        // TODO should this be an actual attribute?
        public AccessLevel AccessLevel(Token accessModifier)
        {
            switch (accessModifier.Kind)
            {
                case TokenKind.PublicKeyword:
                    return Nodes.AccessLevel.Public;
                default:
                    throw new NotSupportedException(accessModifier.ToString());
            }
        }
    }
}