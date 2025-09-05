using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

// See https://aka.ms/new-console-template for more information
const string connectionString = "Endpoint=sb://localhost;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;";
const string queueName = "queue.1";


// Create a ServiceBusClient
await using var client = new ServiceBusClient(connectionString);

// Create a sender for the queue
ServiceBusSender sender = client.CreateSender(queueName);

Console.Write("Enter a message to send: ");
string messageBody = "message one";

ServiceBusMessage message = new ServiceBusMessage(messageBody);

try
{
    await sender.SendMessageAsync(message);
    Console.WriteLine($"Sent: {messageBody}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error sending message: {ex.Message}");
}