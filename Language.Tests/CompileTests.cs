using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Adamant.Tools.Compiler.Bootstrap.Core.Diagnostics;
using Adamant.Tools.Compiler.Bootstrap.Core.Tests;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using Adamant.Tools.Compiler.Bootstrap.Language.Tests.Data;
using Adamant.Tools.Compiler.Bootstrap.Semantics;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Names;
using Adamant.Tools.Compiler.Bootstrap.Semantics.Types;
using Adamant.Tools.Compiler.Bootstrap.Syntax;
using Adamant.Tools.Compiler.Bootstrap.Syntax.Nodes;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Categories;

namespace Adamant.Tools.Compiler.Bootstrap.Language.Tests
{
    [Category("Compile")]
    public class CompileTests
    {
        [Theory]
        [MemberData(nameof(GetAllAnalyzerTestCases))]
        public void Analyzes([NotNull] CompileTestCase testCase)
        {
            var file = testCase.Code.ToFakeCodeFile();
            var tokens = new Lexer().Lex(file);
            var parser = new Parser();
            var compilationUnit = parser.Parse(file, tokens);
            var packageSyntax = new PackageSyntax(compilationUnit.ToSyntaxList());
            var analyzer = new SemanticAnalyzer();
            var package = analyzer.Analyze(packageSyntax).Package;
            AssertSemanticsMatch(testCase.ExpectedSemanticTree, package);
        }

        [Fact]
        public void CanGetAllAnalyzerTestCases()
        {
            Assert.NotEmpty(GetAllAnalyzerTestCases());
        }

        private static void AssertSemanticsMatch(JObject expectedValue, object value)
        {
            // TODO  Finish checking semantics matches expected
            var expectedType = expectedValue.Value<string>("#type").Replace("_", "");
            Assert.True(value.GetType().Name.Equals(expectedType, StringComparison.InvariantCultureIgnoreCase),
                        $"Expected {expectedType}, found {value.GetType().Name}");

            foreach (var property in expectedValue.Properties().Where(p => p.Name != "#type"))
            {
                var expected = property.Value;
                var actual = GetProperty(value, property.Name.Replace("_", ""));
                if (actual is IEnumerable<Diagnostic> diagnostics)
                {
                    Assert.Equal(expected.ToObject<int[]>().OrderBy(x => x), diagnostics.Select(d => d.ErrorCode).OrderBy(x => x));
                    return;
                }
                switch (expected.Type)
                {
                    case JTokenType.Boolean:
                        Assert.Equal(expected.ToObject<bool>(), actual);
                        break;
                    case JTokenType.String:
                        switch (actual)
                        {
                            case string actualString:
                                Assert.Equal(expected.ToObject<string>(), actualString);
                                break;
                            case Name _:
                            case DataType _:
                            case OwnedLifetime _:
                                Assert.Equal(expected.ToObject<string>(), actual.ToString());
                                break;
                            default:
                                Assert.Equal(expected.ToObject(actual.GetType()), actual);
                                break;
                        }
                        break;
                    case JTokenType.Array:
                        var expectedObjects = expected.ToObject<JObject[]>();
                        var actualObjects = (IReadOnlyCollection<object>)actual;
                        Assert.Equal(expectedObjects.Length, actualObjects.Count);
                        foreach (var item in expectedObjects.Zip(actualObjects))
                            AssertSemanticsMatch(item.Item1, item.Item2);
                        break;
                    case JTokenType.Object:
                        AssertSemanticsMatch((JObject)expected, actual);
                        break;
                    case JTokenType.Null:
                        Assert.Null(actual);
                        break;
                    default:
                        throw new NotSupportedException(expected.Type.ToString());
                }
            }
        }

        private static object GetProperty(object value, string name)
        {
            var property = value.GetType().GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            Assert.True(property != null, $"No property '{name}' on type {value.GetType().Name}");
            return property.GetValue(value);
        }

        /// Loads all *.xml test cases for the analyzer.
        public static TheoryData<CompileTestCase> GetAllAnalyzerTestCases()
        {
            var testCases = new TheoryData<CompileTestCase>();
            var testsDirectory = LangTestsDirectory.Get();
            var analyzeTestsDirectory = Path.Combine(testsDirectory, "compile");
            foreach (string testFile in Directory.EnumerateFiles(analyzeTestsDirectory, "*.json", SearchOption.AllDirectories))
            {
                var fullCodePath = Path.ChangeExtension(testFile, "ad");
                var relativeCodePath = Path.GetRelativePath(analyzeTestsDirectory, fullCodePath);
                testCases.Add(new CompileTestCase(fullCodePath, relativeCodePath));
            }
            return testCases;
        }
    }
}