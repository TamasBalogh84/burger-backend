using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Parameters
{
    public class GetAllPlacesParameters
    {
        [FromQuery(Name = "pageNumber")]
        public int PageNumber { get; set; }

        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; }

        [FromQuery(Name = "skipReviews")]
        public bool SkipReviews { get; set; }
    }
}
