﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web_API9.Models;


namespace web_API9.Controllers
{
    [Route("Menu")]
    [ApiController]
    public class MenuController : Controller
    {
        [HttpGet("Menu")]
        public IActionResult Menu()
        {
            return View();
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "JwtBearer")]
        [HttpPost("Post")]
        public string Post()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userName = claim[0].Value;
            var tekst = claim[2].Value;
            return "Welcome to: " + userName + " -> " + tekst;
        }

        
        [Authorize(Roles = "Admin", AuthenticationSchemes = "JwtBearer")]
        [HttpGet("GetValue")]
        public ActionResult<IEnumerable<string>> GetValue()
        {
            return new string[] { "Value1", "Value2", "Value3" };
        }
    }
}
