using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5000");

// builder.Services.AddWebSockets();
var app = builder.Build();
app.UseWebSockets();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws-menu")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await HandleWebSocketConnection(webSocket);
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    }
    else if (context.Request.Path == "/menu")
    {
        // Define the menu items
        var menu = new { items = new[] { "Pizza1", "Pizza2", "Pizza3", "Pasta", "Salad" } };

        // Return the menu as JSON
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(menu));
    }
    else
    {
        await next();
    }
});

app.MapGet("/", () => "WebSocket server is running.");

app.Run();

async Task HandleWebSocketConnection(WebSocket webSocket)
{
    var menu = new { items = new[] { "Pizza1", "Pizza2", "Pizza3", "Pasta", "Salad" } };

    var interval = TimeSpan.FromMinutes(1);
    var cancellationToken = new CancellationTokenSource();

    while (webSocket.State == WebSocketState.Open)
    {
        var json = JsonSerializer.Serialize(menu);
        var bytes = Encoding.UTF8.GetBytes(json);
        var buffer = new ArraySegment<byte>(bytes);

        await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, cancellationToken.Token);

        try
        {
            await Task.Delay(interval, cancellationToken.Token);
        }
        catch (TaskCanceledException)
        {
            break;
        }
        Console.WriteLine($"Sent message: {json}");
    }

    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
}
