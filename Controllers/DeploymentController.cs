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
    
    public class DeploymentController : Controller
    {

        public IActionResult Deployment()
        {

            return View();
        }
    }
}
