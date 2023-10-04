namespace SpecFlowLivingDocParser.Models
{
    public class AppSettings
    {
        public string InputDirectory { get; set; } = string.Empty;
        public string OutputDirectory { get; set; } = string.Empty;
        public string FilePattern { get; set; } = string.Empty;
        public string OutputPath => Path.Combine(OutputDirectory, "TestExecution.json");

        public override string ToString()
        {
            return $"Input Directory: {InputDirectory}\nOutput Directory: {OutputDirectory}\nFile Pattern: {FilePattern}";
        }
    }
}
