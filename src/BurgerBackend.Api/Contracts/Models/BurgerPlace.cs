using System.ComponentModel.DataAnnotations;

namespace BurgerBackend.Api.Contracts.Models;

public record BurgerPlace
{
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    public IEnumerable<Burger> AvailableBurgers { get; init; }

    [Required]
    public string Name { get; init; } = string.Empty;

    [Required]
    public string Information { get; init; } = string.Empty;

    [Required]
    public Location Location { get; init; }

    [Required]
    public IEnumerable<OpeningTime> OpeningTimes { get; init; }

    public IEnumerable<Review>? Reviews { get; init; }
}