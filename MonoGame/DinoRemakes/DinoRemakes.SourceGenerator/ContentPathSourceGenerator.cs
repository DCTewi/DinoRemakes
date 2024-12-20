﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace DinoRemakes.SourceGenerator;

[Generator]
public class ContentPathSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var mcgbPath = context.AdditionalTextsProvider.Where(static file => file.Path.EndsWith(".mgcb"));

        var mcgbContent = mcgbPath.Select(static (text, ct) => text.GetText(ct)!.ToString());

        var provider = mcgbContent.Collect();

        context.RegisterSourceOutput(provider, Execute);
    }

    private static void Execute(SourceProductionContext context, ImmutableArray<string> source)
    {
        StringBuilder result = new();

        result.AppendLine("/// Auto generated by DinoRemakes.SourceGenerator.ContentPathSourceGenerator ///");
        result.AppendLine("namespace DinoRemakes.SourceGenerator;");
        result.AppendLine("public partial class ContentPath");
        result.AppendLine("{");

        foreach (var content in source)
        {
            foreach (var raw in content.Split('\n'))
            {
                var line = raw.Trim();
                if (line.StartsWith("#begin "))
                {
                    var pathLiteral = TrimExtension(line.Substring(7));
                    var splitedPath = pathLiteral.Split('/');
                    for (int i = 0; i < splitedPath.Length - 1; ++i)
                    {
                        for (int j = 0; j <= i; ++j)
                        {
                            result.Append("\t");
                        }
                        result.AppendLine($"public partial class {ConvertPathToUpper(splitedPath[i])}");
                        for (int j = 0; j <= i; ++j)
                        {
                            result.Append("\t");
                        }
                        result.AppendLine("{");
                    }
                    for (int j = 0; j < splitedPath.Length; ++j)
                    {
                        result.Append("\t");
                    }
                    result.AppendLine($"public static readonly string {ConvertPathToUpper(splitedPath[splitedPath.Length - 1])} = @\"{pathLiteral}\";\n");
                    for (int i = 0; i < splitedPath.Length - 1; ++i)
                    {
                        for (int j = splitedPath.Length - 1; j > i; --j)
                        {
                            result.Append("\t");
                        }
                        result.AppendLine("}");
                    }
                }
            }
        }
        result.AppendLine("}");

        context.AddSource("ContentPath.monogame.g.cs", SourceText.From(result.ToString(), Encoding.UTF8, SourceHashAlgorithm.Sha256));
    }

    private static string TrimExtension(string path)
    {
        if (path.IndexOf('.') > 0)
        {
            path = path.Substring(0, path.IndexOf('.'));
        }
        return path;
    }

    private static string ConvertPathToUpper(string path)
    {
        return string.IsNullOrEmpty(path)
            ? string.Empty
            : !char.IsLetter(path[0]) ? $"_{path}" : char.IsLower(path[0]) ? $"{char.ToUpper(path[0])}{path.Substring(1)}" : path;
    }
}
