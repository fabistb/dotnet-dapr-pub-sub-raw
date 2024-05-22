# dotnet-dapr-pub-sub-raw
Sample application to demonstrate the Dapr Pub/Sub behavior with raw mesages.

https://docs.dapr.io/developing-applications/building-blocks/pubsub/pubsub-raw/

# Setup
Simple Asp.Net Core application using _Dapr_ and to _Dapr Sidekick_ to automatically start the sidecar. 

# Requests
All requests can be found in the [request.http](./PubSubRaw/requests/requests.http) file.

The Dapr Api is used to publish a message to the Pub/Sub component to ensure the format is correct.

# CloudEvent Middleware is used
In the _Program.cs_ the _CloudEvents_ middleware is registerd and used.

```csharp
app.UseCloudEvents();
```

## Publish CloudEvent Message
Message is succesfully processed.

```csharp
[Topic("messagebus", "simple-pub-sub")]
```

**Console Output:**
```
MessageId: 1, MessageText: Default PubSub
```

## Publish Raw Message
The _Controller_ is configured with _enableRawPayload = true_.

```csharp
[Topic("messagebus", "simple-pub-sub-raw", true)]
```

**Console Output** 
```
Error processing Redis message 1716405320463-0: retriable error occurred: retriable error returned from app while processing pub/sub event a1cb106b-750d-4a4c-afa4-51ffff131dc9, topic: simple-pub-sub-raw, body: {"type":"https://tools.ietf.org/html/rfc9110#section-15.5.16","title":"Unsupported Media Type","status":415,"traceId":"00-1528c29fb196f50ebc73d7d9ba02b899-18bc49b5c8715820-00"}. status code returned: 415

```

## Publish Raw Message False
The _Controller_ is configured with _enableRawPayload = false_.

```csharp
[Topic("messagebus", "simple-pub-sub-raw", false)]
```

**Console Output**
```
 Error processing Redis message 1716405763461-0: retriable error occurred: retriable error returned from app while processing pub/sub event <nil>, topic: <nil>, body: {"type":"https://tools.ietf.org/html/rfc9110#section-15.5.16","title":"Unsupported Media Type","status":415,"traceId":"00-3bfbeacd78f1331f0531eb9b3991c9aa-89875a0062443781-00"}. status code returned: 415

```

# CloudEvent Middleware isn't used
In the _Program.cs_ the _CloudEvents_ middleware isn't registered and used.

```csharp
// app.UseCloudEvents();
```

## Publish CloudEvent Message
As expected the message isn't succesfully processed.

**Console Output**:
```
Error processing Redis message 1716406257888-0: retriable error occurred: retriable error returned from app while processing pub/sub event 4312e408-1f2c-4f3c-a5f2-b223e0841ae2, topic: simple-pub-sub, body: {"type":"https://tools.ietf.org/html/rfc9110#section-15.5.1","title":"One or more validation errors occurred.","status":400,"errors":{"MessageId":["The MessageId field is required."],"MessageText":["The MessageText field is required."]},"traceId":"00-e98522fc4ab01aed069ffa8d2aba046b-51bea81a73d53da9-01"}. status code returned: 400
```

## Publish Raw Message
Message isn't succesfully processed but shouldn't it process?

**Console Output**:
```
Error processing Redis message 1716406412956-0: retriable error occurred: retriable error returned from app while processing pub/sub event 0548c621-462b-4cad-832f-f7e38d0d2d34, topic: simple-pub-sub-raw, body: {"type":"https://tools.ietf.org/html/rfc9110#section-15.5.1","title":"One or more validation errors occurred.","status":400,"errors":{"MessageId":["The MessageId field is required."],"MessageText":["The MessageText field is required."]},"traceId":"00-1db679e4f35f7f96a91b16f7c4ff123a-ae35215ed131cb29-00"}. status code returned: 400
```

## Publish Raw Message False
Message is successfully processed.

**Console Output**:
```
MessageId: 3, MessageText: Raw PubSub False
```