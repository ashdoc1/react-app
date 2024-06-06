using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Google.Cloud.PubSub.V1;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<PizzaOrderService>();
builder.Services.AddSingleton<PublisherService>();
var app = builder.Build();

app.MapGet("/", () => "Welcome to Pizza Ordering API!");

app.MapPost("/api/orders", async (List<CreateOrderDto> orderDtos, PizzaOrderService orderService, PublisherService publisherService) =>
{
    Console.WriteLine("MapPost function called for /api/orders");

    var orderId = await orderService.CreateOrderAsync(orderDtos);

    // Create a JSON message with the entire order details
    var message = JsonSerializer.Serialize(new { OrderId = orderId, Items = orderDtos });

    // Publish the message to the Pub/Sub topic
    try
    {
        Console.WriteLine("Publishing order...");
        await publisherService.PublishAsync(message);
        Console.WriteLine("Order published successfully");
    }
    catch (Exception ex)
    {
        // Handle publishing error (e.g., log the error, return an error response)
        Console.WriteLine($"Error publishing message: {ex.Message}");
        return Results.Problem(statusCode: 500, title: "Order couldn't be accepted. Try again later");
    }

    return Results.Ok(new { OrderId = orderId, Status = "Order Received" });
});

app.Run();

public record CreateOrderDto(string PizzaId, string PizzaName, string PizzaQuantity, int Quantity);

public class PizzaOrderService
{
    public async Task<Guid> CreateOrderAsync(List<CreateOrderDto> orderDtos)
    {
        // Simulate order creation logic (e.g., store in database, generate order ID)
        await Task.Delay(100); // Simulate async operation
        return Guid.NewGuid();
    }
}

public class PublisherService
{
    private readonly TopicName _topicName;
    private readonly PublisherClient _publisherClient;

    public PublisherService()
    {
        Console.WriteLine("PublisherService constructor called");
        _topicName = new TopicName("gke-proj-1-394220", "order");
        _publisherClient = PublisherClient.CreateAsync(_topicName).Result;
    }

    public async Task PublishAsync(string message)
    {
        var pubsubMessage = new PubsubMessage
        {
            Data = Google.Protobuf.ByteString.CopyFromUtf8(message)
        };
        Console.WriteLine("Publishing message to Pub/Sub...");
        await _publisherClient.PublishAsync(pubsubMessage);
        Console.WriteLine("Published message: " + pubsubMessage);
    }
}

// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.DependencyInjection;
// using System;
// using System.Text.Json;
// using Google.Cloud.PubSub.V1;

// var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddSingleton(new PizzaOrderService());
// builder.Services.AddSingleton<PublisherService>();
// var app = builder.Build();

// app.MapGet("/", () => "Welcome to Pizza Ordering API!");

// app.MapPost("/api/orders", async (CreateOrderDto orderDto, PizzaOrderService orderService, PublisherService publisherService) =>
// {
//     Console.WriteLine("MapPost function called for /api/orders");
//     var orderId = await orderService.CreateOrderAsync(orderDto);

//     // Create a JSON message with order details
//     var message = JsonSerializer.Serialize(new { OrderId = orderId, PizzaName = orderDto.PizzaName });

//     // Publish the message to the Pub/Sub topic
//     try
//     {
//         Console.WriteLine("--------2---------");
//         await publisherService.PublishAsync(message);
//         Console.WriteLine("--------3---------");
//     }
//     catch (Exception ex)
//     {
//         // Handle publishing error (e.g., log the error, return an error response)
//         Console.WriteLine("--------error path 1---------");
//         Console.WriteLine($"Error publishing message: {ex.Message}");
//         return Results.Problem(statusCode: 500, title: "Order couldn't be accepted. try again later");
//     }

//     return Results.Ok(new { OrderId = orderId, Status = "Order Received" });
// });

// app.Run();

// public record CreateOrderDto(string PizzaName);

// public class PizzaOrderService
// {

//     public async Task<Guid> CreateOrderAsync(CreateOrderDto orderDto)
//     {
//         // Simulate order creation logic (e.g., store in database, generate order ID)
//         await Task.Delay(100); // Simulate async operation
//         return Guid.NewGuid();
//     }
// }

// public class PublisherService
// {
//     private readonly TopicName _topicName;
//     //private readonly Task<PublisherClient> _publisherClient;
//     private PublisherClient _publisherClient;

//     public PublisherService()
//     {
//         Console.WriteLine("--------constructor called---------");
//         _topicName = new TopicName("gke-proj-1-394220", "order");
//        // _publisherClient = PublisherClient.Create(_topicName);
//         _publisherClient = PublisherClient.CreateAsync(_topicName).Result;
//     }

//     public async Task PublishAsync(string message)
//     {
//         var pubsubMessage = new PubsubMessage
//         {
//             Data = Google.Protobuf.ByteString.CopyFromUtf8(message)
//         };
//         Console.WriteLine("--------4---------");
//         //await _publisherClient.PublishAsync(_topicName.ToString(), pubsubMessage);
//         await _publisherClient.PublishAsync(pubsubMessage);
//         Console.WriteLine("pubsubMessage"+pubsubMessage);

//     }
// }