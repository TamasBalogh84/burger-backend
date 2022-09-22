using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Parameters;

public class GetReviewsByPlaceIdParameters
{
    [FromRoute(Name = "placeId")]
    [Required]
    public Guid PlaceId { get; set; }
}