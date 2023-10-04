using SpecFlowLivingDocParser.Models;

namespace SpecFlowLivingDocParser.Models
{
    public class LivingDoc
    {
        public IEnumerable<object>? Nodes { get; set; }
        public DateTime? ExecutionTime { get; set; }
        public DateTime? GenerationTime { get; set; }
        public string? PluginUserSpecFlowId { get; set; }
        public object? CLIUserSpecFlowId { get; set; }
        public IEnumerable<ExecutionResult?> ExecutionResults { get; set; } = default!;
        public object? StepReports { get; set; }
    }
}