using System.Collections.Generic;
using System.Linq;
using Adamant.Tools.Compiler.Bootstrap.AST;
using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.IntermediateLanguage.ControlFlow;
using Adamant.Tools.Compiler.Bootstrap.Metadata.Types;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Errors;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics.Borrowing
{
    public class BorrowChecker
    {
        private readonly CodeFile file;
        private readonly Diagnostics diagnostics;
        private int nextObjectId = 1;

        private int NewObjectId()
        {
            var objectId = nextObjectId;
            nextObjectId += 1;
            return objectId;
        }

        private BorrowChecker(CodeFile file, Diagnostics diagnostics)
        {
            this.file = file;
            this.diagnostics = diagnostics;
        }

        public static void Check(
            IEnumerable<MemberDeclarationSyntax> declarations,
            Diagnostics diagnostics)
        {
            foreach (var declaration in declarations)
            {
                var borrowChecker = new BorrowChecker(declaration.File, diagnostics);
                borrowChecker.Check(declaration);
            }
        }

        public void Check(MemberDeclarationSyntax declaration)
        {
            switch (declaration)
            {
                case TypeDeclarationSyntax typeDeclaration:
                    Check(typeDeclaration);
                    break;
                case FunctionDeclarationSyntax function:
                    Check(function);
                    break;
                default:
                    throw NonExhaustiveMatchException.For(declaration);
            }
        }

        private static void Check(TypeDeclarationSyntax _type)
        {
            // Currently nothing to check
        }

        private void Check(FunctionDeclarationSyntax function)
        {
            // We can't do borrow checking because of errors or no body
            if (function.Poisoned || function.ControlFlow == null)
                return;

            var variables = function.ControlFlow.VariableDeclarations;
            // Do borrow checking with claims
            var blocks = new Queue<BasicBlock>();
            blocks.Enqueue(function.ControlFlow.EntryBlock);
            var claims = new Claims();
            // Compute parameter claims once in case for some reason we process the entry block repeatedly
            var parameterClaims = AcquireParameterClaims(function);

            while (blocks.TryDequeue(out var block))
            {
                var claimsBeforeStatement = ClaimsBeforeBlock(function, block, claims, parameterClaims);

                foreach (var statement in block.ExpressionStatements)
                {
                    var claimsAfterStatement = claims.After(statement);
                    claimsAfterStatement.UnionWith(claimsBeforeStatement);

                    // Create/drop any claims modified by the statement
                    switch (statement)
                    {
                        case AssignmentStatement assignmentStatement:
                            AcquireClaims(assignmentStatement.Place, assignmentStatement.Value, claimsBeforeStatement, claimsAfterStatement, variables);
                            break;
                        case ActionStatement actionStatement:
                            AcquireClaims(null, actionStatement.Value, claimsBeforeStatement, claimsAfterStatement, variables);
                            break;
                        case DeleteStatement deleteStatement:
                        {
                            var title = GetTitle(deleteStatement.Place.CoreVariable(), claimsBeforeStatement);
                            CheckCanMove(title.ObjectId, claimsBeforeStatement, deleteStatement.Span);
                            claimsAfterStatement.RemoveWhere(c => c.Variable == title.Variable);
                            break;
                        }
                        default:
                            throw NonExhaustiveMatchException.For(statement);
                    }

                    // TODO drop claims due to liveness

                    // Get Ready for next statement
                    claimsBeforeStatement = claimsAfterStatement;
                }

                var claimsAfterBlock = claims.After(block.Terminator);
                if (!claimsBeforeStatement.SetEquals(claimsAfterBlock))
                {
                    claimsAfterBlock.UnionWith(claimsBeforeStatement);
                    switch (block.Terminator)
                    {
                        case IfStatement ifStatement:
                            blocks.Enqueue(function.ControlFlow.BasicBlocks[ifStatement.ThenBlockNumber]);
                            blocks.Enqueue(function.ControlFlow.BasicBlocks[ifStatement.ElseBlockNumber]);
                            break;
                        case GotoStatement gotoStatement:
                            blocks.Enqueue(function.ControlFlow.BasicBlocks[gotoStatement.GotoBlockNumber]);
                            break;
                        case ReturnStatement _: // Add only applies to copy types so no loans
                            break;
                        default:
                            throw NonExhaustiveMatchException.For(block.Terminator);
                    }
                }
            }
        }

        private HashSet<Claim> ClaimsBeforeBlock(
            FunctionDeclarationSyntax function,
            BasicBlock block,
            Claims claims,
            HashSet<Claim> parameterClaims)
        {
            var claimsBeforeStatement = new HashSet<Claim>();

            if (block == function.ControlFlow.EntryBlock)
                claimsBeforeStatement.UnionWith(parameterClaims);

            foreach (var predecessor in function.ControlFlow.Edges.To(block).Select(b => b.Terminator))
                claimsBeforeStatement.UnionWith(claims.After(predecessor));

            return claimsBeforeStatement;
        }

        private HashSet<Claim> AcquireParameterClaims(FunctionDeclarationSyntax function)
        {
            var claimsBeforeStatement = new HashSet<Claim>();
            foreach (var parameter in function.ControlFlow.VariableDeclarations
                .Where(v => v.IsParameter && v.Type is ReferenceType))
            {
                claimsBeforeStatement.Add(new Loan(parameter.Number, nextObjectId));
                nextObjectId += 1;
            }

            return claimsBeforeStatement;
        }

        private void CheckCanMove(int objectId, HashSet<Claim> claims, TextSpan span)
        {
            var cantTake = claims.OfType<Loan>().SelectMany(l => l.Restrictions)
                .Any(r => r.Place == objectId && !r.CanTake);

            if (cantTake)
                diagnostics.Add(BorrowError.BorrowedValueDoesNotLiveLongEnough(file, span));
        }

        private void AcquireClaims(
            Place assignToPlace,
            Value value,
            HashSet<Claim> claimsBeforeStatement,
            HashSet<Claim> claimsAfterStatement,
            FixedList<LocalVariableDeclaration> variables)
        {
            switch (value)
            {
                case ConstructorCall constructorCall:
                    foreach (var argument in constructorCall.Arguments)
                        AcquireClaim(assignToPlace, argument, claimsBeforeStatement, claimsAfterStatement);
                    // We have made a new object, assign it a new id
                    var objectId = NewObjectId();
                    // Variable acquires title on any new objects
                    var title = new Title(assignToPlace.CoreVariable(), objectId);
                    claimsAfterStatement.Add(title);
                    break;
                case UnaryOperation unaryOperation:
                    AcquireClaim(assignToPlace, unaryOperation.Operand, claimsBeforeStatement, claimsAfterStatement);
                    break;
                case BinaryOperation binaryOperation:
                    AcquireClaim(assignToPlace, binaryOperation.LeftOperand, claimsBeforeStatement, claimsAfterStatement);
                    AcquireClaim(assignToPlace, binaryOperation.RightOperand, claimsBeforeStatement, claimsAfterStatement);
                    break;
                case IntegerConstant _:
                    // no loans to acquire
                    break;
                case Operand operand:
                    AcquireClaim(assignToPlace, operand, claimsBeforeStatement, claimsAfterStatement);
                    break;
                case FunctionCall functionCall:
                    if (functionCall.Self != null)
                        AcquireClaim(assignToPlace, functionCall.Self, claimsBeforeStatement,
                            claimsAfterStatement);
                    foreach (var argument in functionCall.Arguments)
                        AcquireClaim(assignToPlace, argument, claimsBeforeStatement,
                            claimsAfterStatement);
                    AcquireOwnershipIfMoved(assignToPlace, variables, claimsAfterStatement);
                    break;
                case VirtualFunctionCall virtualFunctionCall:
                    if (virtualFunctionCall.Self != null)
                        AcquireClaim(assignToPlace, virtualFunctionCall.Self, claimsBeforeStatement, claimsAfterStatement);
                    foreach (var argument in virtualFunctionCall.Arguments)
                        AcquireClaim(assignToPlace, argument, claimsBeforeStatement, claimsAfterStatement);
                    AcquireOwnershipIfMoved(assignToPlace, variables, claimsAfterStatement);
                    break;
                default:
                    throw NonExhaustiveMatchException.For(value);
            }
        }

        private void AcquireOwnershipIfMoved(
            Place assignToPlace,
            FixedList<LocalVariableDeclaration> variables,
            HashSet<Claim> claimsAfterStatement)
        {
            if (assignToPlace == null) return;
            var assignToVariable = variables[assignToPlace.CoreVariable()];
            if (assignToVariable.Type is ReferenceType referenceType
                && referenceType.IsOwned)
            {
                // We have taken ownership of a new object, assign it a new id
                var objectId = NewObjectId();
                // Variable acquires title on any new objects
                var title = new Title(assignToPlace.CoreVariable(), objectId);
                claimsAfterStatement.Add(title);
            }
        }

        private static void AcquireClaim(
            Place assignToPlace,
            Operand operand,
            HashSet<Claim> claimsBeforeStatement,
            HashSet<Claim> claimsAfterStatement)
        {
            switch (operand)
            {
                case Place place:
                    var coreVariable = place.CoreVariable();
                    var claim = claimsBeforeStatement.SingleOrDefault(t => t.Variable == coreVariable);
                    // Copy types don't have claims right now
                    if (claim != null && assignToPlace != null) // copy types don't have claims right now
                    {
                        var loan = new Loan(assignToPlace.CoreVariable(),
                            operand,
                            claim.ObjectId);
                        if (!claimsAfterStatement.Contains(loan))
                            claimsAfterStatement.Add(loan);
                    }
                    break;
                case Constant _:
                    // no loans to acquire
                    break;
                default:
                    throw NonExhaustiveMatchException.For(operand);
            }
        }

        private static Title GetTitle(int variable, HashSet<Claim> claims)
        {
            return claims.OfType<Title>().Single(t => t.Variable == variable);
        }
    }
}
