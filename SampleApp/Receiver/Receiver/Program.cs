// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;

const string connectionString = "Endpoint=sb://localhost;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;";
const string queueName = "queue.1";

// Create a ServiceBusClient
await using var client = new ServiceBusClient(connectionString);

// Create a processor that we can use to process the messages
ServiceBusProcessor processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

processor.ProcessMessageAsync += MessageHandler;
processor.ProcessErrorAsync += ErrorHandler;

await processor.StartProcessingAsync();

Console.WriteLine("Receiving messages. Press any key to exit...");
Console.ReadKey();

await processor.StopProcessingAsync();


static async Task MessageHandler(ProcessMessageEventArgs args)
{
    string body = args.Message.Body.ToString();
    Console.WriteLine($"Received: {body}");

    // Complete the message. Messages are removed from the queue after this.
    await args.CompleteMessageAsync(args.Message);
}

static Task ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine($"Error: {args.Exception.Message}");
    return Task.CompletedTask;
}