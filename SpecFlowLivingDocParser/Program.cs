using Newtonsoft.Json;
using SpecFlowLivingDocParser.Builders;
using SpecFlowLivingDocParser.Models;
using SpecFlowLivingDocParser.Parsers;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            var settings = ParseCommandLineArguments(args);
            var livingDocObjects = ParseSpecFlowTestExecutionFiles(settings);
            var executionResults = ConsolidateExecutionResults(livingDocObjects);
            var newLivingDocObject = BuildLivingDoc(livingDocObjects, executionResults);

            SaveLivingDocToJSON(newLivingDocObject, settings.OutputPath);
            Console.WriteLine($"JSON file created: '{settings.OutputPath}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static AppSettings ParseCommandLineArguments(string[] args)
    {
        var supportedParameters = new Dictionary<string, bool>()
        {
            { "inputDir", true },
            { "outputDir", true },
            { "filePattern", false }
        };

        var arguments = ArgumentParser.GetArguments(args: args, parameters: supportedParameters);

        var appSettings = new AppSettings()
        {
            InputDirectory = Path.GetFullPath(arguments["inputDir"]),
            OutputDirectory = Path.GetFullPath(arguments["outputDir"]),
            FilePattern = arguments.ContainsKey("filePattern")
                          ? arguments["filePattern"]
                          : "TestExecution*.json"
        };

        Console.WriteLine(appSettings.ToString());

        if (!Directory.Exists(appSettings.InputDirectory) || !Directory.Exists(appSettings.OutputDirectory))
            throw new DirectoryNotFoundException("Input and/or output directory not found");

        return appSettings;
    }

    private static IEnumerable<LivingDoc> ParseSpecFlowTestExecutionFiles(AppSettings appSettings)
    {
        return ExecutionFileParser.Parse(path: appSettings.InputDirectory, filePattern: appSettings.FilePattern);
    }

    private static IEnumerable<ExecutionResult> ConsolidateExecutionResults(IEnumerable<LivingDoc> documents)
    {
        return ExecutionResultsParser.ConsolidateResults(documents);
    }

    private static LivingDoc BuildLivingDoc(IEnumerable<LivingDoc> documents, IEnumerable<ExecutionResult> executionResults)
    {
        return LivingDocBuilder.Build(documents: documents, executionResults: executionResults);
    }

    private static void SaveLivingDocToJSON(LivingDoc livingDoc, string outputPath)
    {
        var livingDocJSONString = JsonConvert.SerializeObject(livingDoc);
        File.WriteAllText(outputPath, livingDocJSONString);
    }
}