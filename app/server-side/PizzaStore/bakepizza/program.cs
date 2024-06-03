using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
       // Console.log("baker service program")
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
            });
}


// using System;
// using System.Linq;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;
// using Google.Cloud.PubSub.V1;
// using Grpc.Core;
// using Google.Protobuf;

// class BakePizza
// {
//     private static readonly string ProjectId = Environment.GetEnvironmentVariable("PROJECT_ID");
//     private static readonly string orderTopicSubscriptionId = Environment.GetEnvironmentVariable("ORDER_TOPIC_SUBSCRIPTION_ID");
//     private static readonly string notificationTopicId = Environment.GetEnvironmentVariable("NOTIFICATION_TOPIC_ID");

//     static async Task Main(string[] args)
//     {
//         var subscriptionName = SubscriptionName.FromProjectSubscription(ProjectId, orderTopicSubscriptionId);
//         var topicName = TopicName.FromProjectTopic(ProjectId, notificationTopicId);

//         SubscriberServiceApiClient subscriberClient = await SubscriberServiceApiClient.CreateAsync();
//         PublisherServiceApiClient publisherClient = await PublisherServiceApiClient.CreateAsync();

//         // Create a SimpleSubscriber for the subscription.
//         var subscriber = await SubscriberClient.CreateAsync(subscriptionName);

//         // Define the message handler
//         Task startTask = subscriber.StartAsync(async (PubsubMessage orderMsg, CancellationToken cancellationToken) =>
//         {
//             // Display the received message
//             string messageText = orderMsg.Data.ToStringUtf8();
//             Console.WriteLine($"Received message: {messageText}");

//             // Define the messages to publish
//             string[] messages = { "preparing", "baking", "ready" };

//             // Publish the messages sequentially with a 1-minute delay
//             foreach (string message in messages)
//             {
//                 // Construct the message
//                 var pubsubMessage = new PubsubMessage
//                 {
//                     Data = ByteString.CopyFromUtf8(message)
//                 };

//                 // Publish the message
//                 await publisherClient.PublishAsync(topicName, new[] { pubsubMessage });
//                 Console.WriteLine($"Published message '{message}' to topic {notificationTopicId}");

//                 // Wait for 1 minute
//                 await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
//             }

//             // Acknowledge the message
//             return SubscriberClient.Reply.Ack;
//         });

//         // Wait for the subscriber to process messages
//         Console.WriteLine("Listening for messages...");
//         await startTask;

//         // Stop the subscriber after use
//         await subscriber.StopAsync(CancellationToken.None);
//     }
// }