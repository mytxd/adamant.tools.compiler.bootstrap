using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Adamant.Tools.Compiler.Bootstrap.API;
using Adamant.Tools.Compiler.Bootstrap.Core;
using Adamant.Tools.Compiler.Bootstrap.Emit.C;
using Adamant.Tools.Compiler.Bootstrap.Forge.Config;

namespace Adamant.Tools.Compiler.Bootstrap.Forge.Build
{
    /// <summary>
    /// Represents and determines an order to build packages in
    /// </summary>
    internal class BuildSequence : IEnumerable<Project>
    {
        private readonly List<Project> projects = new List<Project>();

        public void Add(Project project)
        {
            projects.Add(project);
            // TODO sort them somehow?
        }

        public IEnumerator<Project> GetEnumerator()
        {
            return projects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Build(bool verbose)
        {
            var compiler = new AdamantCompiler();
            foreach (var project in projects)
            {
                Console.WriteLine($"Compiling {project.Name} ({project.Path})");
                var sourceDir = Path.Combine(project.Path, "src");
                var sourcePaths = Directory.EnumerateFiles(sourceDir, "*.ad", SearchOption.AllDirectories);
                var codeFiles = sourcePaths.Select(CodeFile.Load).ToList();
                var package = compiler.CompilePackage(codeFiles);
                var diagnostics = package.AllDiagnostics();
                if (diagnostics.Any())
                {
                    foreach (var diagnostic in diagnostics.OrderBy(d => d.File.Reference.ToString()).ThenBy(d => d.Position))
                    {
                        Console.WriteLine($"{diagnostic.File.Reference}:{diagnostic.Position.Line}:{diagnostic.Position.Column} {diagnostic.Info.Level} {diagnostic.Info.ErrorCode}");
                        Console.WriteLine(diagnostic.Info.Message);
                    }
                    return;
                }
                var cCode = new CEmitter().Emit(package);
                var cacheDir = Path.Combine(project.Path, ".forge-cache");
                Directory.CreateDirectory(cacheDir); // Ensure the cache directory exists
                var outputPath = Path.Combine(cacheDir, "program.c");
                File.WriteAllText(outputPath, cCode, Encoding.ASCII);
            }
        }
    }
}