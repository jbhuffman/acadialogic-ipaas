using System.ComponentModel.DataAnnotations;

namespace Pinecone.Core.Models;

public class QueueOptions
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Connection { get; set; } = string.Empty;
}
