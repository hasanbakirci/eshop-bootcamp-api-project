using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using eshop.API.Models;
using eshop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace eshop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        [HttpGet("login")]
       public IActionResult Login(UserLogin userLogin){
           var user = new UserService().IsValid(userLogin.Username, userLogin.Password);
           if(user == null){
               return BadRequest("Kullanıcı adı ya da şifre ...");
           }
           var claims = new[]{
               new Claim(JwtRegisteredClaimNames.Sub, user.Name),
               new Claim(ClaimTypes.Role, user.Role)
           };
           var issuer = "hasan";
           var audience = "hasan";
           var key = "qweqweqweqweqweqweasdasdsawqexzcxvxcvxcsdfsdrew";
           var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
           var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

           var token = new JwtSecurityToken(issuer: issuer,
           audience:audience,claims:claims,notBefore:DateTime.Now,expires:DateTime.Now.AddMinutes(20),
           signingCredentials:credential);

           return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});
       } 
    }
}