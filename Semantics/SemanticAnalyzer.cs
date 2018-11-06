using System.Collections.Generic;
using System.Linq;
using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Analysis;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Analyzers;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Declarations;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Names;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Semantics
{
    public class SemanticAnalyzer
    {
        [NotNull]
        public Package Analyze(
            [NotNull] PackageSyntax packageSyntax,
            [NotNull] IReadOnlyDictionary<string, Package> references)
        {
            var nameBuilder = new NameBuilder();

            // Gather a list of all the namespaces for validating using statements
            var namespacesInPackage = new NamespaceBuilder(nameBuilder).GatherNamespaces(packageSyntax).ToList();

            // Gather all the declarations and simultaneously build up trees of lexical scopes
            var compilationUnits = new AnalysisBuilder(nameBuilder).Build(packageSyntax).ToList();

            // Check lexical scopes and attach to entities etc.
            var scopeBinder = new ScopeBinder(compilationUnits, nameBuilder, references);
            foreach (var scope in compilationUnits.Select(cu => cu.GlobalScope))
                scopeBinder.BindCompilationUnitScope(scope);

            // Make a list of all the member declarations
            var declarationAnalyses = compilationUnits.SelectMany(cu => cu.MemberDeclarations).ToList();

            // Do name binding, type checking, IL statement generation and compile time code execution
            // They are all interdependent to some degree
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
            return new Package(packageSyntax.Name, diagnostics.Build(), namespacesInPackage, declarations, entryPoint);
        }

        [CanBeNull]
        private static FunctionDeclaration DetermineEntryPoint([NotNull] List<Declaration> declarations, [NotNull] DiagnosticsBuilder diagnostics)
        {
            var mainFunctions = declarations.OfType<FunctionDeclaration>()
                .Where(f => f.Name is SimpleName name && name.Text == "main")
                .ToList();

            // TODO warn on and remove main functions that don't have correct parameters or types

            // TODO compiler error on multiple main functions

            return mainFunctions.SingleOrDefault();
        }
    }
}
