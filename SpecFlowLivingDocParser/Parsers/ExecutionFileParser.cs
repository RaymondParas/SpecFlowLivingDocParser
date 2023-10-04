using Newtonsoft.Json;
using SpecFlowLivingDocParser.Models;

namespace SpecFlowLivingDocParser.Parsers
{
    public static class ExecutionFileParser
    {
        public static IEnumerable<LivingDoc> Parse(string path, string filePattern)
        {
            var executionFiles = Directory.GetFiles(path, filePattern);

            if (executionFiles.Length == 0)
                throw new FileNotFoundException("No test execution JSON files found in specified input directory");

            Console.WriteLine($"Test execution files found: '{executionFiles.Length}'");

            var livingDocs = new List<LivingDoc>();
            foreach (var file in executionFiles)
            {
                try
                {
                    using var reader = new StreamReader(file);
                    var json = reader.ReadToEnd();
                    var doc = JsonConvert.DeserializeObject<LivingDoc>(json);

                    if (doc != null)
                        livingDocs.Add(doc);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{file}': {ex.Message}");
                    throw;
                }
            }

            return livingDocs;
        }
    }
}
