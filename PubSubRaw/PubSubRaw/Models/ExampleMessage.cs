using System.ComponentModel.DataAnnotations;

namespace PubSubRaw.Models;

public record ExampleMessage(
    [Required] int? MessageId, 
    [Required] string MessageText);