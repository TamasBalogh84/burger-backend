using BurgerBackend.Api.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Parameters
{
    public class CreatePlaceParameters
    {
        [FromBody]
        public CreateBurgerPlace? Place { get; set; }
    }
}
