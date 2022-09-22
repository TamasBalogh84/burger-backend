using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Parameters;

public class DeleteReviewParameters
{
    [FromRoute(Name = "placeId")]
    [Required]
    public Guid PlaceId { get; set; }

    [FromRoute(Name = "reviewId")]
    [Required]
    public Guid ReviewId { get; set; }
}