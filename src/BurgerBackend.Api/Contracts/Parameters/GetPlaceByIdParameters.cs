using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Parameters
{
    public class GetPlaceByIdParameters
    {
        [FromRoute(Name = "placeId")]
        [Required]
        public Guid PlaceId { get; set; }

        public override string ToString()
        {
            return $"{nameof(PlaceId)}: {PlaceId}";
        }
    }
}
