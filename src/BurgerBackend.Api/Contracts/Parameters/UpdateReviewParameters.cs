using System.ComponentModel.DataAnnotations;
using BurgerBackend.Api.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Parameters
{
    public class UpdateReviewParameters
    {
        public UpdateReviewParameters(Review review)
        {
            Review = review;
        }

        [FromRoute(Name = "placeId")]
        [Required]
        public Guid PlaceId { get; set; }

        [FromRoute(Name = "reviewId")]
        [Required]
        public Guid ReviewId { get; set; }

        [FromBody]
        [Required]
        public Review Review { get; }
    }
}
