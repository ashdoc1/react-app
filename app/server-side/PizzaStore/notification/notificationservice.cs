using Microsoft.AspNetCore.Hosting;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Logging;



// public class using Microsoft.AspNetCore.Builder;

public class NotificationService
{
    private static readonly string[] _menuItems = new[] { "Pizza1-ready", "Pizza2-ready", "Pizza3-ready", "Pasta-ready", "Salad-ready" };

    // private readonly ILogger<NotificationService> _logger;
    private string _psProjectId;
    private readonly string _notifSubscriptionId;
    private readonly string _notifTopicId;
    private SubscriberClient _subscriberClient;
    public event EventHandler<string> MessageReceived;


    // public NotificationService()
    // {

    //     // Get environment variables
    //     _psProjectId = Environment.GetEnvironmentVariable("PS_PROJECT_ID");
    //     _notifSubscriptionId = Environment.GetEnvironmentVariable("NOTIF_SUBSCRIPTION_ID");
    //     _notifTopicId = Environment.GetEnvironmentVariable("NOTIF_TOPIC_ID");

    //     // _logger.LogInformation($"PS_PROJECT_ID: {_psProjectId}");
    //     // _logger.LogInformation($"ORDER_SUBSCRIPTION_ID: {_notifSubscriptionId}");
    //     // _logger.LogInformation($"NOTIF_TOPIC_ID: {_notifTopicId}");

    //     Console.WriteLine($"-------NotificationService-----");
    //     Console.WriteLine($"PS_PROJECT_ID: {_psProjectId}");
    //     Console.WriteLine($"ORDER_SUBSCRIPTION_ID: {_notifSubscriptionId}");
    //     Console.WriteLine($"NOTIF_TOPIC_ID: {_notifTopicId}");
    //     // _notifTopicName = new TopicName(_psProjectId, _notifTopicId);


    //     // Validate environment variables
    //     if (string.IsNullOrEmpty(_psProjectId))
    //     {
    //         throw new ArgumentException("Environment variable PS_PROJECT_ID is not set.");
    //     }
    //     if (string.IsNullOrEmpty(_notifSubscriptionId))
    //     {
    //         throw new ArgumentException("Environment variable ORDER_SUBSCRIPTION_ID is not set.");
    //     }
    //     if (string.IsNullOrEmpty(_notifTopicId))
    //     {
    //         throw new ArgumentException("Environment variable NOTIF_TOPIC_ID is not set.");
    //     }

    // }

    // public async Task InitializeAsync()
    // {
    //     //_subscriberClient = await SubscriberClient.CreateAsync(SubscriptionName.FromProjectSubscription(_psProjectId, _notifSubscriptionId));        
    //     _subscriberClient = await SubscriberClient.CreateAsync(SubscriptionName.FromProjectSubscription(_psProjectId, _notifSubscriptionId));
    // }

    // public String ReadMessagesAsync()
    // {
    //     Console.WriteLine("inside ReadMessagesAsync ");
    //     string messageText = "";
    //     try{
    //         _subscriberClient.StartAsync(async (PubsubMessage message, CancellationToken ct) =>
    //         {
    //             // Process the message here.
    //             string messageText = message.Data.ToStringUtf8();
    //             Console.WriteLine($"NotificationService - Received message: {messageText}");

    //             // Acknowledge the message.
    //             return SubscriberClient.Reply.Ack;
    //         });
    //     }
    //     catch(Exception ex){
    //         Console.WriteLine($"NotificationService-->: {ex}");
    //     }
    //     return messageText;
    // }

    // public async Task ReadMessagesAsync()
    // {
    //     try{
    //         await _subscriberClient.StartAsync(async (PubsubMessage message, CancellationToken ct) =>
    //         {
    //             // Process the message here.
    //             string messageText = message.Data.ToStringUtf8();
    //             Console.WriteLine($"NotificationService - Received message: {messageText}");

    //             // Raise the event with the message text.
    //             MessageReceived?.Invoke(this, messageText);

    //             // Acknowledge the message.
    //             return SubscriberClient.Reply.Ack;
    //         });
    //     }
    //     catch(Exception ex){
    //         Console.WriteLine($"NotificationService: {ex.Message}");
    //     }
    // }

    // public async Task ReadMessagesAsync()
    // {
    //     using var subscription = await _pubSubClient.GetSubscriptionAsync(_notifSubscriptionId);
    //     var pullResponse = await subscription.PullAsync(returnImmediately: true);

    //     foreach (var receivedMessage in pullResponse.ReceivedMessages)
    //     {
    //         // Process the message here.
    //         Console.WriteLine($"NotificationService - Received message: {Encoding.UTF8.GetString(receivedMessage.Message.Data.ToArray())}");

    //         // Acknowledge the message.
    //         await subscription.AcknowledgeAsync(receivedMessage.AckId);
    //     }
    // }
    public string GetNextNotification()
    {
        var random = new Random();
        var index = random.Next(_menuItems.Length);
        return _menuItems[index];
    }
}