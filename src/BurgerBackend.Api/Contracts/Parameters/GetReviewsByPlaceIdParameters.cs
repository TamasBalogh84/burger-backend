using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Parameters;

public class GetReviewsByPlaceIdParameters
{
    [FromRoute(Name = "placeId")]
    [Required]
    public Guid PlaceId { get; set; }

    [FromQuery(Name = "pageNumber")]
    public int PageNumber { get; set; }

    [FromQuery(Name = "pageSize")]
    public int PageSize { get; set; }
}