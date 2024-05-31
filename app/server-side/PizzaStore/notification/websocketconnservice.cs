using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
// using static pizzaservice;


public class WebSocketConnService
{
    

    public async Task HandleWebSocketConnection(WebSocket webSocket)
    {
        var interval = TimeSpan.FromSeconds(10);
        var cancellationToken = new CancellationTokenSource();
        var notifService = new NotificationService();

        Console.WriteLine("inside HandleWebSocketConnection");
        while (webSocket.State == WebSocketState.Open)
        {
            string item = notifService.GetNextNotification();
            var json = JsonSerializer.Serialize(item);
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


}
// {
//     public async Task HandleWebSocketConnection(WebSocket webSocket)
//     {
//         var menu = new { items = new[] { "Pizza1", "Pizza2", "Pizza3", "Pasta", "Salad" } };

//         var interval = TimeSpan.FromMinutes(1);
//         var cancellationToken = new CancellationTokenSource();

//         while (webSocket.State == WebSocketState.Open)
//         {
//             var json = JsonSerializer.Serialize(menu);
//             var bytes = Encoding.UTF8.GetBytes(json);
//             var buffer = new ArraySegment<byte>(bytes);

//             await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, cancellationToken.Token);

//             try
//             {
//                 await Task.Delay(interval, cancellationToken.Token);
//             }
//             catch (TaskCanceledException)
//             {
//                 break;
//             }
//             Console.WriteLine($"Sent message: {json}");
//         }

//         await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
//     }
// }
