using System.ComponentModel.DataAnnotations;

namespace BurgerBackend.Api.Contracts.Models;

public class BurgerPlace
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public IEnumerable<Burger> AvailableBurgers { get; set; }

    [Required]
    public string Information { get; set; } = string.Empty;

    [Required]
    public Location Location { get; set; }

    [Required]
    public string OpeningTime { get; set; } = string.Empty;

    public IEnumerable<Review> Reviews { get; set; }
}