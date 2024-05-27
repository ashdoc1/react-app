using Microsoft.AspNetCore.Hosting;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

// public class using Microsoft.AspNetCore.Builder;

public class PizzaService
{
    private static readonly string[] _menuItems = new[] { "Pizza1", "Pizza2", "Pizza3", "Pasta", "Salad" };

    public string GetNextMenuItem()
    {
        var random = new Random();
        var index = random.Next(_menuItems.Length);
        return _menuItems[index];
    }
}