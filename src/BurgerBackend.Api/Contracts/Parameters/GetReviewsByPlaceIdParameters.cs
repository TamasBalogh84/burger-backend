using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Parameters;

public class GetReviewsByPlaceIdParameters
{
    private const int MaxPageSize = 99;

    private int _pageSize = 10;

    [FromRoute(Name = "placeId")]
    [Required]
    public Guid PlaceId { get; set; }

    [FromQuery(Name = "pageNumber")] 
    public int PageNumber { get; set; } = 1;

    [FromQuery(Name = "pageSize")]
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}