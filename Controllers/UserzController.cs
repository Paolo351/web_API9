using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web_API9.Models;

namespace web_API9.Controllers
{
    [Route("Userz")]
    [ApiController]
    public class UserzController : Controller
    {

        [Route("Show_userz")]
        public IActionResult Show_userz()
        {

            return View();
        }
    }
}
