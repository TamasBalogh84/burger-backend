using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Parameters
{
    public class GetReviewByIdParameters
    {
        [FromRoute(Name = "reviewId")]
        [Required]
        public Guid ReviewId { get; set; }
    }
}
