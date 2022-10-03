using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BurgerBackend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    /// <summary>  
    /// Generate Json Web Token Method  
    /// </summary>  
    /// <param name="userInfo"></param>  
    /// <returns></returns>  
    private string GenerateJSONWebToken(LoginModel userInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisisasecretkey@123"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            "https://localhost:7081",
          "https://localhost:7081",
          new List<Claim>(),
          expires: DateTime.Now.AddMinutes(120),
          signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>  
    /// Hardcoded the User authentication  
    /// </summary>  
    /// <param name="login"></param>  
    /// <returns></returns>  
    private async Task<LoginModel> AuthenticateUser(LoginModel login)
    {
        LoginModel user = null;

        if (login.UserName == "testuser")
        {
            user =  new LoginModel { UserName = "testuser", Password = "test123pass" };
        }
        return user;
    }

    /// <summary>  
    /// Login Authenticaton using JWT Token Authentication  
    /// </summary>  
    /// <param name="data"></param>  
    /// <returns></returns>  
    [AllowAnonymous]
    [HttpPost(nameof(Login))]
    public async Task<IActionResult> Login([FromBody] LoginModel data)
    {
        IActionResult response = Unauthorized();
        var user = await AuthenticateUser(data);
        if (data != null)
        {
            var tokenString = GenerateJSONWebToken(user);
            response = Ok(new { Token = tokenString, Message = "Success" });
        }
        return response;
    }
}

public class LoginModel
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}