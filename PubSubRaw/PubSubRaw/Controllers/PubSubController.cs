using Dapr;
using Microsoft.AspNetCore.Mvc;
using PubSubRaw.Models;

namespace PubSubRaw.Controllers;

[ApiController]
[Route("api/v1.0/[controller]")]
public class PubSubController(ILogger<PubSubController> logger) : ControllerBase
{
    [Topic("messagebus", "simple-pub-sub")]
    [HttpPost("simple-pub-sub")]
    public ActionResult ProcessPubSubMessage([FromBody] ExampleMessage message)
    {
        logger.LogInformation($"MessageId: {message.MessageId}, MessageText: {message.MessageText}");

        return Ok();
    }

    [Topic("messagebus", "simple-pub-sub-raw", enableRawPayload: true)]
    [HttpPost("wimple-pub-sub-raw")]
    public ActionResult ProcessPubSubMessageRaw([FromBody] ExampleMessage message)
    {
        logger.LogInformation($"MessageId: {message.MessageId}, MessageText: {message.MessageText}");

        return Ok();
    }
    
    [Topic("messagebus", "simple-pub-sub-raw-false", enableRawPayload: false)]
    [HttpPost("wimple-pub-sub-raw-false")]
    public ActionResult ProcessPubSubMessageRawFalse([FromBody] ExampleMessage message)
    {
        logger.LogInformation($"MessageId: {message.MessageId}, MessageText: {message.MessageText}");

        return Ok();
    }
}