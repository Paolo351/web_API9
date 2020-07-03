using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using web_API9.Models;
using web_API9.Models.Application.Home;
using web_API9.Models.Application.User;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;


namespace web_API9.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _config;

        private User AuthenticateUser(User login)
        {
            User user = null;

            //For demo I am using static user info
            if (login.Email == "janowak@gmail.com" && login.PasswordHash == "123")
            {
                user = new User("Jan", "Nowak", "123", "janowak@gmail.com", (UserRole)1);

            }

            return user;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public HomeController(IConfiguration config, ILogger<HomeController> logger)
            
        {
            _config = config;

            _logger = logger;
        }

        [HttpGet("Login")]
        public IActionResult Login(string Email, string PasswordHash)
        {

            User login = new User
            {
                Email = Email,

                PasswordHash = PasswordHash
            };

            IActionResult response = Unauthorized();

            var user = AuthenticateUser(login);

            var viewModel = new TokenViewModel();

            if (user != null)
            {
                var tokenStr = GenerateJSONWebToken(user);

                response = Ok(new { token = tokenStr });

                viewModel = new TokenViewModel()
                {
                    TokenString = tokenStr
                };
            }        

            return View(viewModel);

            //return response;
            //return RedirectToAction("NotDelUser", "User", new { UserId });
            //return RedirectToAction("Menu", "Menu", new { response = response });
        }

        private string GenerateJSONWebToken(User userinfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userinfo.Email),
                new Claim(JwtRegisteredClaimNames.Email, userinfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Spectator"),
                new Claim(ClaimTypes.Role, "Schema_guard")
            };

            


            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodetoken;
        }

        

        private readonly ILogger<HomeController> _logger;


        [HttpGet("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

