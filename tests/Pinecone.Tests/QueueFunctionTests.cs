using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Pinecone.Core.Models;
using Pinecone.Functions;

using Xunit;

namespace Pinecone.Tests;

public class QueueFunctionTests
{
    [Fact]
    public async Task Run_LogsMessage()
    {
        var options = Options.Create(new QueueOptions
        {
            Name = "queue-1",
            Connection = "UseDevelopmentStorage=true"
        });

        var logger = new TestLogger<QueueFunction>();
        var function = new QueueFunction(options, logger);

        await function.Run("hello-world");

        Assert.Contains(logger.Messages, m => m.Contains("queue-1") && m.Contains("hello-world"));
    }

    private sealed class TestLogger<T> : ILogger<T>
    {
        public List<string> Messages { get; } = new();

        public IDisposable BeginScope<TState>(TState state) => NullScope.Instance;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (formatter != null)
            {
                Messages.Add(formatter(state, exception));
            }
        }

        private sealed class NullScope : IDisposable
        {
            public static readonly NullScope Instance = new();
            public void Dispose() { }
        }
    }
}
