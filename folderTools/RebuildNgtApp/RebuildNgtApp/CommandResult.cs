namespace RebuildNgtApp
{
    public struct CommandResult
    {
        public int ExitCode { get; set; }
        public string Output { get; set; }

        public CommandResult(int exitCode, string output)
        {
            ExitCode = exitCode;
            Output = output;
        }
    }
}