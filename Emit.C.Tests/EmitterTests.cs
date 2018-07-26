using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Emit.C.Tests.Data;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Semantics;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Nodes;
using Adamant.Tools.Compiler.Bootstrap.Syntax;
using Xunit;
using Xunit.Categories;

namespace Adamant.Tools.Compiler.Bootstrap.Emit.C.Tests
{
    public class EmitterTests
    {
        [Theory]
        [Category("Emitter")]
        [MemberData(nameof(GetAllEmitterTestCases))]
        public void Emits(EmitterTestCase testCase)
        {
            var package = Compile(testCase);
            var code = new Code();
            new CEmitter().Emit(package.CompilationUnits.Single(), code);
            AssertEmittedCodeMatches(testCase.Expected, code);
        }

        private static Package Compile(EmitterTestCase testCase)
        {
            var codePath = new CodePath(testCase.RelativeCodePath);
            var code = new CodeText(testCase.Code);
            var tokens = new Lexer().Lex(code);
            var parser = new Parser();
            var syntaxTree = parser.Parse(codePath, code, tokens);
            var packageSyntax = new PackageSyntax(syntaxTree.Yield());
            var analyzer = new SemanticAnalyzer();
            return analyzer.Analyze(packageSyntax);
        }

        private void AssertEmittedCodeMatches(string expected, Code code)
        {
            var sectionsStarts = Regex.Matches(expected, "//#")
                .Select(m => m.Index)
                .ToList();
            for (var i = 0; i < sectionsStarts.Count; i++)
            {
                var startOfHeader = sectionsStarts[i];
                var startOfSectionName = startOfHeader + 3;
                var endOfHeader = expected.IndexOf('\n', startOfHeader) + 1;
                var endOfSection = i < sectionsStarts.Count - 1 ? sectionsStarts[i + 1] : expected.Length;
                var sectionName = expected.Substring(startOfSectionName, endOfHeader - startOfSectionName).Trim();
                var expectedCode = expected.Substring(endOfHeader, endOfSection - endOfHeader);
                var actualCode = GetCode(code, sectionName.Replace("_", ""));
                Assert.Equal(expectedCode, actualCode);
            }
        }

        private string GetCode(Code code, string name)
        {
            var property = typeof(Code).GetField(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            Assert.True(property != null, $"No code section named '{name}'");
            return ((CCodeBuilder)property.GetValue(code)).Code;
        }

        [Fact]
        [Category("Emitter")]
        public void CanGetAllEmitterTestCases()
        {
            Assert.NotEmpty(GetAllEmitterTestCases());
        }

        public static TheoryData<EmitterTestCase> GetAllEmitterTestCases()
        {
            var testCases = new TheoryData<EmitterTestCase>();
            var currentDirectory = Directory.GetCurrentDirectory();
            foreach (string testFile in Directory.EnumerateFiles(currentDirectory, "*.c", SearchOption.AllDirectories))
            {
                var fullCodePath = Path.ChangeExtension(testFile, "ad");
                var relativeCodePath = Path.GetRelativePath(currentDirectory, fullCodePath);
                testCases.Add(new EmitterTestCase(fullCodePath, relativeCodePath));
            }
            return testCases;
        }
    }
}