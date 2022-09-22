namespace BurgerBackend.Api.Contracts.Models;

public class Burger
{
    public string Name { get; set; } = string.Empty;

    public decimal? Price { get; set; }
}