//using Microsoft.AspNetCore.Connections;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Pinecone.Core.Models;

namespace Pinecone.Functions;

public class QueueFunction
{
    private readonly ILogger<QueueFunction> _logger;
    private readonly QueueOptions _options;

    public QueueFunction(IOptions<QueueOptions> options, ILogger<QueueFunction> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    // Triggers on messages in the "event-work" Storage Queue
    [Function("QueueTriggerDemo")]
    public Task Run(
        [QueueTrigger("%Queue:Name%", Connection = "AzureWebJobsStorage")] string message)
    {
        _logger.LogInformation("Queue {Queue} processed message: {Message}", _options.Name, message);

        return Task.CompletedTask;
    }
}
