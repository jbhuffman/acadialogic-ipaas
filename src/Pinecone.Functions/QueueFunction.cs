using Microsoft.AspNetCore.Connections;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Pinecone.Functions;

public class QueueFunction
{
    private readonly ILogger<QueueFunction> _logger;

    public QueueFunction(ILogger<QueueFunction> logger)
    {
        _logger = logger;
    }

    // Triggers on messages in the "event-work" Storage Queue
    [Function("QueueTriggerDemo")]
    public void Run(
        [QueueTrigger("event-work", Connection = "AzureWebStorage")]
        string message)
    {
        _logger.LogInformation("Queue message processed: {Message}", message);
    }
}
