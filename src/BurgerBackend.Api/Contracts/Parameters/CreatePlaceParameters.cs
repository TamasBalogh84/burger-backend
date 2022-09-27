using System.ComponentModel.DataAnnotations;
using BurgerBackend.Api.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Api.Contracts.Parameters
{
    public class CreatePlaceParameters
    {
        [FromBody]
        [Required]
        public BurgerPlace? Place { get; set; }
    }
}
