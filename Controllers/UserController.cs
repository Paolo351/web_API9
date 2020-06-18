using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web_API9.Models;
using web_API9.Infrastructure;
using web_API9.Models.Application.Database;
using web_API9.Models.Application.Deployment;
using web_API9.Models.Application.Project;
using web_API9.Models.Application.User;


namespace web_API9.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : Controller
    {


        private readonly Userservice _Userservice;

        public UserController(Userservice Userservice)
        {
            _Userservice = Userservice;
        }


        
        [HttpGet("ShowAllUser")]
        public IActionResult ShowAllUser()
        {

            var User_list = new List<User>(_Userservice.Get());

            var viewModel = new ShowAllUserViewModel()
            {
                Users = User_list
            };

            return View(viewModel);

        }

        
        [HttpGet("AddUser")]
        public IActionResult AddUser(string firstname_wpis, string lastname_wpis, string password_wpis, string email_wpis, string rola_wpis)
        {
            var uzer = new User(firstname_wpis, lastname_wpis, password_wpis, email_wpis, rola_wpis);

            var User_list = new List<User>();
            User_list.Add(_Userservice.Create(uzer));
            var viewModel = new ShowAllUserViewModel()
            {
                Users = User_list
            };
            return View(viewModel);
        }

        
        [HttpGet("DelUser")]
        public IActionResult DelUser(string numer_wpis)
        {
            var User = _Userservice.Get(numer_wpis);
            var User_list = new List<User>();
            User_list.Add(User);



            if (User == null)
                return NotFound();

            var viewModel = new ShowAllUserViewModel()
            {
                Users = User_list
            };

            _Userservice.Remove(User.UserId);


            return View(viewModel);
        }

        
        [HttpGet("ShowUser")]
        public IActionResult ShowUser()
        {

            return View();
        }
    }
}
