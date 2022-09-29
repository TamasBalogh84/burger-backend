namespace BurgerBackend.Api.Contracts.Models;

public class Location
{
    public string City { get; init; } = string.Empty;

    public string Address { get; init; } = string.Empty;

    public string Coordinates { get; init; } = string.Empty;
}