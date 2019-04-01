using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication6.Models;
using WebApplication6.Services;

namespace WebApplication6.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        
        private readonly UserService _userService;
        
        public AuthenticationController(UserService userService)
        {
            _userService = userService;
        }
        
        [AllowAnonymous]
        public IActionResult  Post(
            [FromBody]AuthenticationRequest authRequest,
            [FromServices] IJwtSigningEncodingKey signingEncodingKey)
//        public IActionResult  Get([FromServices] IJwtSigningEncodingKey signingEncodingKey)
        {
            var user = _userService.Get(authRequest.Login);
            var error = string.Empty;

            if (user == null)
            {
                error = "user not found";
            }
            else{
                if (user.Password != authRequest.Password){
                    error = "user not found";
                }
            }

            if (error.Length > 0)
            {
                return BadRequest(new { error = error}); 
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Login)
            };
 
       
            var token = new JwtSecurityToken(
                issuer: "http://localhost:63342",
                audience: "http://localhost:4200",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: new SigningCredentials(
                    signingEncodingKey.GetKey(),
                    signingEncodingKey.SigningAlgorithm)
            );
            
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new {tokenKey = jwtToken});
        }
    }

}