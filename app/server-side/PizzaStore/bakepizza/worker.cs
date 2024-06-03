using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly string psProjectId;
    private readonly string orderSubscriptionId;
    private SubscriberClient _subscriberClient;
    private readonly string notifTopicId;
    private PublisherClient _publisherClient;
    private readonly TopicName _notifTopicName;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;

        // Get environment variables
        psProjectId = Environment.GetEnvironmentVariable("PS_PROJECT_ID");
        orderSubscriptionId = Environment.GetEnvironmentVariable("ORDER_SUBSCRIPTION_ID");
        notifTopicId = Environment.GetEnvironmentVariable("NOTIF_TOPIC_ID");

        _logger.LogInformation($"PS_PROJECT_ID: {psProjectId}");
        _logger.LogInformation($"ORDER_SUBSCRIPTION_ID: {orderSubscriptionId}");
        _logger.LogInformation($"NOTIF_TOPIC_ID: {notifTopicId}");

        _notifTopicName = new TopicName(psProjectId, notifTopicId);


        // Validate environment variables
        if (string.IsNullOrEmpty(psProjectId))
        {
            throw new ArgumentException("Environment variable PS_PROJECT_ID is not set.");
        }
        if (string.IsNullOrEmpty(orderSubscriptionId))
        {
            throw new ArgumentException("Environment variable ORDER_SUBSCRIPTION_ID is not set.");
        }
        if (string.IsNullOrEmpty(notifTopicId))
        {
            throw new ArgumentException("Environment variable NOTIF_TOPIC_ID is not set.");
        }
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _subscriberClient = await SubscriberClient.CreateAsync(SubscriptionName.FromProjectSubscription(psProjectId, orderSubscriptionId));
        _publisherClient = await PublisherClient.CreateAsync(_notifTopicName);
        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _subscriberClient.StartAsync(async (PubsubMessage message, CancellationToken ct) =>
        {
            _logger.LogInformation($"Received message: {message.Data.ToStringUtf8()}");
            // Process the message

            string orderId = message.Data.ToStringUtf8();
            _logger.LogInformation($"orderId in Received message: {orderId}");
            // Process the order
            await ProcessOrder(orderId, ct);
            //await Task.Delay(2000, ct); // Simulate some processing

            return SubscriberClient.Reply.Ack;
        });

        await Task.Delay(Timeout.Infinite, stoppingToken); // Keep the background service running
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _subscriberClient.StopAsync(cancellationToken);
        await base.StopAsync(cancellationToken);
    }

    // private async Task ProcessOrder(string orderId, CancellationToken ct){
    //     _logger.LogInformation($"Received message: {orderId}");
    // } 

    private async Task ProcessOrder(string orderId, CancellationToken ct)
    {
        _logger.LogInformation("inside ProcessOrder");

        for (int i = 0; i < 5; i++)
        {
            string messageText;
            string ordProcesingStatus;
            switch (i)
            {
                case 0:
                    messageText = $"Order {orderId} is preparing.";
                    ordProcesingStatus = "preparing";
                    break;
                case 1:
                    messageText = $"Order {orderId} is baking.";
                    ordProcesingStatus = "baking";
                    break;
                case 2:
                    messageText = $"Order {orderId} is packing.";
                    ordProcesingStatus = "packing";
                    break;
                case 3:
                    messageText = $"Order {orderId} is ready for delivery.";
                    ordProcesingStatus = "ready-for-delivery";
                    break;
                case 4:
                    messageText = $"Order {orderId} is out for delivery.";
                    ordProcesingStatus = "out-for-delivery";
                    break;
                default:
                    messageText = $"Order {orderId} is unknown.";
                    ordProcesingStatus = "unknown";
                    break;
            }
            _logger.LogInformation("------inside ProcessOrder---2---------------");
            _logger.LogInformation(messageText);
            await publishOrderStatus(orderId, ordProcesingStatus, ct);
            await Task.Delay(2000, ct); // Simulate some processing
        }
    }

    private async Task publishOrderStatus(string orderId,string ordProcesingStatus, CancellationToken ct)
    {
        // _logger.LogInformation(messageText);
        _logger.LogInformation("------inside publishOrderStatus---1---------------");

        // Create a JSON object with order ID, status, and timestamp
        var orderStatus = new
        {
            orderId = orderId,
            status = ordProcesingStatus,
            timestamp = DateTime.UtcNow
        };

        // Serialize the JSON object to a string
        var jsonOrderStatus = JsonConvert.SerializeObject(orderStatus);
        _logger.LogInformation($"------jsonOrderStatus---:{jsonOrderStatus}");
        // Create a new Pub/Sub message with the JSON string as data

        var ordProcessingStatus = new PubsubMessage
        {
            Data = Google.Protobuf.ByteString.CopyFromUtf8(jsonOrderStatus)
        };
        _logger.LogInformation($"------ordProcessingStatus---:{ordProcessingStatus}");
        await _publisherClient.PublishAsync(ordProcessingStatus);
        // Console.WriteLine("pubsubMessage"+pubsubMessage);
        _logger.LogInformation("------inside publishOrderStatus---end---------------");
    }

}