using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using RefactorThis.Models;

namespace RefactorThis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase 
    {
        [HttpGet("{username}/{password}")]
        public IActionResult Login(string username, string password) 
        {
            Guid APIToken = AuthenticateUser.GetAPIToken(username, password);

            if(APIToken==null)
            {
                return StatusCode(401, (new { error = "Username or password incorrect, cannot authenticate user." }));
            }

            return Ok(APIToken);

        }
    }
}