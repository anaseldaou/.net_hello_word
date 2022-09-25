using Microsoft.Extensions.Options;

namespace WebApplication1.Logger
{
    [ProviderAlias("RoundTheCodeFile")]
    public class FileLoggerProvider: ILoggerProvider
    {
        public readonly FileLoggerOptions Options;

        public SemaphoreSlim WriteFileLock;

        public FileLoggerProvider(IOptions<FileLoggerOptions> _options)
        {
            WriteFileLock = new SemaphoreSlim(1, 1);
            Options = _options.Value;

            if (!Directory.Exists(Options.FolderPath))
            {
                Directory.CreateDirectory(Options.FolderPath);
            }
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(this);
        }

        public void Dispose()
        {
        }
    }
}
