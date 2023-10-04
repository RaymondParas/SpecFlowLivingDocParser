namespace SpecFlowLivingDocParser.Parsers
{
    public static class ArgumentParser
    {
        public static IDictionary<string, string> GetArguments(string[] args, IDictionary<string, bool> parameters, string parameterPrefix = "--")
        {
            var requiredParameters = GetRequiredParameters(parameters);
            var arguments = new Dictionary<string, string>();

            for (int index = 0; index < args.Length - 1; index++)
            {
                var arg = args[index];
                if (!arg.StartsWith(parameterPrefix))
                    continue;

                var parameterName = arg.Substring(parameterPrefix.Length);
                if (!parameters.ContainsKey(parameterName))
                    throw new ArgumentException($"'{parameterName}' is not a supported parameter");

                var parameterValue = args[index + 1];
                if (string.IsNullOrWhiteSpace(parameterValue))
                    throw new ArgumentException($"'{parameterName}' does not have a valid argument");

                arguments[parameterName] = parameterValue;
            }

            ValidateRequiredParameters(parameters: arguments.Keys.ToList(),
                                       requiredParameters: requiredParameters);

            return arguments;
        }

        private static IEnumerable<string> GetRequiredParameters(IDictionary<string, bool> parameters)
        {
            return parameters.Where(p => p.Value)
                             .Select(p => p.Key)
                             .ToList();
        }

        private static void ValidateRequiredParameters(IEnumerable<string> parameters, IEnumerable<string> requiredParameters)
        {
            var missingParameters = requiredParameters.Except(parameters)
                                                      .ToList();

            if (missingParameters.Any())
            {
                var message = "Missing required parameters: " + string.Join(", ", missingParameters);
                throw new ArgumentException(message);
            }
        }
    }
}
