using System.ComponentModel.DataAnnotations;
using BurgerBackend.Api.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Parameters;

public class CreateReviewParameters
{
    public CreateReviewParameters()
    {
    }

    public CreateReviewParameters(CreateReview review)
    {
        Review = review;
    }

    [FromRoute(Name = "placeId")]
    [Required]
    public Guid PlaceId { get; set; }

    [FromBody]
    [Required]
    public CreateReview Review { get; set; }
}