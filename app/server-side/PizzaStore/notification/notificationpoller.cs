using Google.Cloud.PubSub.V1;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class NotificationPoller
{
    private readonly string _psProjectId;
    private readonly string _notifSubscriptionId;

    public NotificationPoller()
    {
        // _psProjectId = projectId ?? throw new ArgumentNullException(nameof(projectId));
        // _notifSubscriptionId = subscriptionId ?? throw new ArgumentNullException(nameof(subscriptionId));
        // Get environment variables
        _psProjectId = Environment.GetEnvironmentVariable("PS_PROJECT_ID");
        _notifSubscriptionId = Environment.GetEnvironmentVariable("NOTIF_SUBSCRIPTION_ID");
        // _notifTopicId = Environment.GetEnvironmentVariable("NOTIF_TOPIC_ID");

        Console.WriteLine($"-------NotificationPoller-----");
        Console.WriteLine($"PS_PROJECT_ID: {_psProjectId}");
        Console.WriteLine($"ORDER_SUBSCRIPTION_ID: {_notifSubscriptionId}");
        // Console.WriteLine($"NOTIF_TOPIC_ID: {_notifTopicId}");

    }

    public async Task<List<PubsubMessage>> PullMessagesAsync(int maxMessages = 10)
    {
        SubscriptionName subscriptionName = SubscriptionName.FromProjectSubscription(_psProjectId, _notifSubscriptionId);
        SubscriberServiceApiClient subscriberService = await SubscriberServiceApiClient.CreateAsync();
        
        List<PubsubMessage> messages = new List<PubsubMessage>();

        // Pull messages from the subscription
        try
        {
            PullResponse response = await subscriberService.PullAsync(subscriptionName, returnImmediately: false, maxMessages: maxMessages);

            if (response.ReceivedMessages.Count > 0)
            {
                foreach (ReceivedMessage receivedMessage in response.ReceivedMessages)
                {
                    // Add the message to the list
                    messages.Add(receivedMessage.Message);
                }
            }
            else
            {
                Console.WriteLine("No messages available at this time.");
            }
        }
        catch (RpcException e)
        {
            Console.WriteLine($"Error pulling messages-1: {e}");
            Console.WriteLine($"Error pulling messages-2: {e.Status.Detail}");
        }

        return messages;
    }

    public async Task AcknowledgeMessagesAsync(IEnumerable<string> ackIds)
    {
        SubscriptionName subscriptionName = SubscriptionName.FromProjectSubscription(_psProjectId, _notifSubscriptionId);
        SubscriberServiceApiClient subscriberService = await SubscriberServiceApiClient.CreateAsync();
        await subscriberService.AcknowledgeAsync(subscriptionName, ackIds);
    }
}
