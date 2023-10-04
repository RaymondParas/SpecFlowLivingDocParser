using SpecFlowLivingDocParser.Enums;
using SpecFlowLivingDocParser.Models;

namespace SpecFlowLivingDocParser.Parsers
{
    public static class ExecutionResultsParser
    {
        public static IEnumerable<ExecutionResult> ConsolidateResults(IEnumerable<LivingDoc> documents)
        {
            var filteredDocuments = FilterDocumentsWithExecutionResults(documents);

            var processedExecutionResults = new Dictionary<string, ExecutionResult>();
            var scenariosProcessed = new HashSet<string>();

            foreach (var document in filteredDocuments)
            {
                foreach (var result in document.ExecutionResults)
                {
                    if (result == null
                        || string.IsNullOrEmpty(result?.ScenarioTitle) 
                        || scenariosProcessed.Contains(result.ScenarioTitle)
                        || (processedExecutionResults.ContainsKey(result.ScenarioTitle)
                            && result.Status == null))
                        continue;

                    processedExecutionResults[result.ScenarioTitle] = result;

                    if (result.Status == Status.OK.ToString())
                        scenariosProcessed.Add(result.ScenarioTitle);
                }
            }

            if (processedExecutionResults.Count == 0)
                throw new InvalidOperationException("No valid execution results");

            var consolidatedResults = processedExecutionResults.Values.ToList();

            return consolidatedResults;
        }

        private static IEnumerable<LivingDoc> FilterDocumentsWithExecutionResults(IEnumerable<LivingDoc> documents)
        {
            return documents
                .Where(doc => doc.ExecutionResults != null)
                .ToList();
        }
    }
}
