using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web_API9.Models;
using web_API9.Services;
using web_API9.Models.ViewModels;

namespace web_API9.Controllers
{
    [Route("Userz")]
    [ApiController]
    public class UserzController : Controller
    {

        
        private readonly UserzService _UserzService;

        public UserzController(UserzService UserzService)
        {
            _UserzService = UserzService;
        }


        [Route("Show_all_userz")]
        public IActionResult Show_all_userz()
        {

            var userz_list = new List<Userz>(_UserzService.Get());

            var viewModel = new Show_all_userzViewModel()
            {
                Userzs = userz_list
            };

            return View(viewModel);

        }

        [Route("Show_userz")]
        public IActionResult Show_userz()
        {

            return View();
        }
    }
}
