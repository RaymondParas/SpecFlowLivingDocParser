using SpecFlowLivingDocParser.Models;

namespace SpecFlowLivingDocParser.Builders
{
    public static class LivingDocBuilder
    {
        public static LivingDoc Build(IEnumerable<LivingDoc> documents, IEnumerable<ExecutionResult> executionResults)
        {
            var generationTime = documents?
                .FirstOrDefault(doc => doc.GenerationTime != null)?
                .GenerationTime;

            var pluginUserSpecFlowId = documents?
                .FirstOrDefault(doc => doc.PluginUserSpecFlowId != null)?
                .PluginUserSpecFlowId;

            var livingDoc = new LivingDoc()
            {
                Nodes = new List<object>(),
                ExecutionTime = DateTime.Now,
                GenerationTime = generationTime,
                PluginUserSpecFlowId = pluginUserSpecFlowId,
                CLIUserSpecFlowId = null,
                ExecutionResults = executionResults,
                StepReports = null
            };

            return livingDoc;
        }
    }
}
