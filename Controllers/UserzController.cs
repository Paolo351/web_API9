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

        [Route("Add_userz")]
        public IActionResult Add_userz(string firstname_wpis, string lastname_wpis, string password_wpis, string email_wpis, string rola_wpis)
        {
            var uzer = new Userz(firstname_wpis, lastname_wpis, password_wpis, email_wpis, rola_wpis);

            var userz_list = new List<Userz>();
            userz_list.Add(_UserzService.Create(uzer));
            var viewModel = new Show_all_userzViewModel()
            {
                Userzs = userz_list
            };
            return View(viewModel);
        }

        [Route("Del_userz")]
        public IActionResult Del_userz(string numer_wpis)
        {
            var userz = _UserzService.Get(numer_wpis);
            var userz_list = new List<Userz>();
            userz_list.Add(userz);



            if (userz == null)
                return NotFound();

            var viewModel = new Show_all_userzViewModel()
            {
                Userzs = userz_list
            };

            _UserzService.Remove(userz.UserzId);


            return View(viewModel);
        }

        [Route("Show_userz")]
        public IActionResult Show_userz()
        {

            return View();
        }
    }
}
