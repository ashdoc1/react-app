using Microsoft.AspNetCore.Hosting;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

// public class using Microsoft.AspNetCore.Builder;

public class NotificationService
{
    private static readonly string[] _menuItems = new[] { "Pizza1-ready", "Pizza2-ready", "Pizza3-ready", "Pasta-ready", "Salad-ready" };

    public string GetNextNotification()
    {
        var random = new Random();
        var index = random.Next(_menuItems.Length);
        return _menuItems[index];
    }
}