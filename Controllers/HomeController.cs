using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using web_API9.Infrastructure;
using web_API9.Models;
using web_API9.Models.Application.Home;
using web_API9.Models.Application.User;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using MongoDB.Bson;

namespace web_API9.Controllers
{
    //[Route("Home")]
    //[ApiController]
    public class HomeController : Controller
    {
        private IConfiguration _config;
        private readonly Userservice _Userservice;
        private class Odp
        {
            public string Access_token { get; set; }
            public string UserName { get; set; }
        }

        private  User AuthenticateUser(User login)
        {
            User user = null;

            //var User_list = new List<User>(_Userservice.Get());

            

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

        public HomeController(Userservice Userservice, IConfiguration config, ILogger<HomeController> logger)
            
        {
            _config = config;
            _Userservice = Userservice;
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = "JwtBearer")]
        [AllowAnonymous]
        [HttpPost("Home/Login")]
        public async Task<IActionResult> Login(string Email, string PasswordHash)
        {
            

            User login = new User
            {
                Email = Email,

                PasswordHash = PasswordHash
            };

            

            //IActionResult response = Unauthorized();

            var user = AuthenticateUser(login);

            //var viewModel = new TokenViewModel();

            if (user != null)
            {
                //Odp odp = JsonSerializer.Deserialize<Odp>(await GenerateJSONWebToken(user));

                var tokenStr = await GenerateJSONWebToken(user);

                //var tokenStr = odp.Access_token;

                //AuthenticationHeaderValue authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

                //response = Ok(new { token = tokenStr });

                /*viewModel = new TokenViewModel()
                {
                    TokenString = tokenStr
                };*/
                using (var httpClient = new HttpClient())
                {
                    //httpClient.DefaultRequestHeaders.Authorization
                    //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenStr);
                    //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + tokenStr);
                    httpClient.BaseAddress = new Uri("https://localhost:5001/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + tokenStr);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                return new ObjectResult(tokenStr);
            }
            else
                return BadRequest();

            //return View(viewModel);


            //return response;
            //return RedirectToAction("NotDelUser", "User", new { UserId });
            //return RedirectToAction("Menu", "Menu", new { response = response });

            //Request.Headers.
            
        }

        private async Task<dynamic> GenerateJSONWebToken(User userinfo)
        {
            

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims =  new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userinfo.Email),
                new Claim(JwtRegisteredClaimNames.Email, userinfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(ClaimTypes.Role, "Schema_guard"),
                //new Claim(ClaimTypes.Role, "Spectator"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            


            var token =  new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            

            
            /*await TestAsync();
            var output = await Task.Run(() => new 
            //var output = new
            {
                Access_token = new JwtSecurityTokenHandler().WriteToken(token),

                UserName = userinfo.Email
            });*/
            
            return await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token));
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

//#pragma warning disable CS1998 // Metoda asynchroniczna nie zawiera operatorów „await” i zostanie uruchomiona synchronicznie
//        private async Task TestAsync()
//#pragma warning restore CS1998 // Metoda asynchroniczna nie zawiera operatorów „await” i zostanie uruchomiona synchronicznie

//        {

//            int i = 0;
//            i = i + 1;

//        }
    }
}

