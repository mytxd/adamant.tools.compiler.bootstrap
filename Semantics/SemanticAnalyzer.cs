using System.Collections.Generic;
using System.Linq;
using Adamant.Tools.Compiler.Bootstrap.Core.Diagnostics;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Analysis;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Analysis.Declarations;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Borrowing;
using Adamant.Tools.Compiler.Bootstrap.Semantics.ControlFlow;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Declarations;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Names;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Scopes;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Types;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics
{
    public class SemanticAnalyzer
    {
        [NotNull]
        public Package Analyze([NotNull] PackageSyntax packageSyntax)
        {
            var nameBuilder = new NameBuilder();

            // Gather a list of all the namespaces for validating using statements
            var namespaces = new NamespaceBuilder(nameBuilder).GatherNamespaces(packageSyntax).ToList();

            // Gather all the declarations and simultaneously build up trees of lexical scopes
            var compilationUnits = new AnalysisBuilder(nameBuilder).Build(packageSyntax).ToList();

            // Make a list of all the declarations
            var declarationAnalyses = compilationUnits.SelectMany(cu => cu.Declarations).ToList();

            // Check lexical scopes and attach to entities etc.
            var scopeBinder = new ScopeBinder(declarationAnalyses);
            foreach (var scope in compilationUnits.Select(cu => cu.GlobalScope))
                scopeBinder.Bind(scope);

            // Do name binding, type checking, IL statement generation and compile time code execution
            // They are all interdependent to some degree
            var nameBinder = new NameBinder();
            var typeChecker = new TypeChecker();
            typeChecker.CheckDeclarations(declarationAnalyses);

            // At this point, some but not all of the functions will have IL statements generated,
            // now generate the rest
            var cfgBuilder = new ControlFlowGraphBuilder();
            cfgBuilder.BuildGraph(declarationAnalyses.OfType<FunctionDeclarationAnalysis>());

            // Only borrow checking left
            var borrowChecker = new BorrowChecker();
            borrowChecker.Check(declarationAnalyses);

            // Gather the diagnostics and declarations into a package
            var diagnostics = new DiagnosticsBuilder();
            // First pull over all the lexer and parser errors from the compilation units
            foreach (var compilationUnit in packageSyntax.CompilationUnits)
                diagnostics.Publish(compilationUnit.Diagnostics);

            var declarations = declarationAnalyses.Select(d => d.Complete(diagnostics)).Where(d => d != null).ToList();
            var entryPoint = DetermineEntryPoint(declarations, diagnostics);
            return new Package(packageSyntax.Name, diagnostics.Build(), namespaces, declarations, entryPoint);
        }

        [CanBeNull]
        private static FunctionDeclaration DetermineEntryPoint([NotNull] List<Declaration> declarations, [NotNull] DiagnosticsBuilder diagnostics)
        {
            var mainFunctions = declarations.OfType<FunctionDeclaration>()
                // TODO make an easy way to construct and compare qualified names
                .Where(f => !f.QualifiedName.Qualifier.Any()
                            && f.QualifiedName.Name.Text == "main")
                .ToList();

            // TODO warn on and remove main functions that don't have correct parameters or types

            // TODO compiler error on multiple main functions

            return mainFunctions.SingleOrDefault();
        }
    }
}
