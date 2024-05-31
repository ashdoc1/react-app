using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
// builder.WebHost.UseUrls("http://localhost:5000");
// builder.Services.AddWebSockets();
var app = builder.Build();
app.UseWebSockets();

app.Use(async (context, next) =>
{
    Console.WriteLine($"path: {context.Request.Path}");
    if (context.Request.Path == "/ws-notif")
    {
        Console.WriteLine($"context.WebSockets.IsWebSocketRequest: {context.WebSockets.IsWebSocketRequest}");
        Console.WriteLine("------1-----");
        if (context.WebSockets.IsWebSocketRequest)
        {
            Console.WriteLine("------2-----");
            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            Console.WriteLine("web socket connection established");
            var service = new WebSocketConnService();
            Console.WriteLine("web socket connection established");
            await service.HandleWebSocketConnection(webSocket);
        }
        else
        {
            context.Response.StatusCode = 400;
        }
        Console.WriteLine($"context.Response.StatusCode: {context.Response.StatusCode}");
    }
    else if (context.Request.Path == "/menu")
    {
        // Define the menu items
        var pizzamenu = new { items = new[] { "Pizza1", "Pizza2", "Pizza3", "Pasta", "Salad" } };

        // Return the menu as JSON
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(pizzamenu));
    }
    else
    {
        await next();
    }
});

app.MapGet("/", () => "WebSocket server is running.");

app.Run();
