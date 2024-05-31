// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

// app.Run();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Define the menu items
var menu = new List<Pizza>()
{
    new Pizza { Name = "Margherita", Ingredients = "Tomato sauce, mozzarella cheese", Price = 10 },
    new Pizza { Name = "Pepperoni", Ingredients = "Tomato sauce, mozzarella cheese, pepperoni", Price = 12 },
    new Pizza { Name = "Hawaiian", Ingredients = "Tomato sauce, mozzarella cheese, ham, pineapple", Price = 14 },
    // Add more pizzas here...
};

// Endpoint to return the menu as JSON
app.MapGet("/menu", () => Results.Json(menu));

app.Run();

// Pizza class definition
public class Pizza
{
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public decimal Price { get; set; }
}


