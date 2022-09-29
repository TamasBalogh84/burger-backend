using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Parameters
{
    public class GetAllPlacesParameters
    {
        private const int MaxPageSize = 99;

        private int _pageSize = 10;

        [FromQuery(Name = "pageNumber")] 
        public int PageNumber { get; set; } = 1;

        [FromQuery(Name = "pageSize")]
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        [FromQuery(Name = "skipReviews")]
        public bool SkipReviews { get; set; }
    }
}
