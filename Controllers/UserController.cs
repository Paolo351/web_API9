﻿using System;
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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace web_API9.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly Userservice _Userservice;
        private readonly DeploymentService _DeploymentService;
        private readonly ProjectService _ProjectService;
        private readonly DatabaseService _DatabaseService;

        public UserController(Userservice Userservice, DatabaseService DatabaseService, DeploymentService DeploymentService, ProjectService ProjectService)
        {
            _Userservice = Userservice;
            _ProjectService = ProjectService;
            _DatabaseService = DatabaseService;
            _DeploymentService = DeploymentService;
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
        public IActionResult AddUser(string FirstName, string LastName, string PasswordHash, string Email, UserRole Role)
        {
            var uzer = new User(FirstName, LastName, PasswordHash, Email, Role);

            var User_list = new List<User>();

            User_list.Add(_Userservice.Create(uzer));

            var viewModel = new ShowAllUserViewModel()
            {
                Users = User_list
            };

            return View(viewModel);
        }


        [HttpGet("DelUser")]
        public IActionResult DelUser(string UserId)
        {
            var all_deployments_list = new List<Deployment>(_DeploymentService.Get());

            var deployments_z_userem_do_kasacji = new List<Deployment>();

            foreach (var document in all_deployments_list)
            {
                if (document.SchemaCreatedByUserId == UserId)
                {
                    deployments_z_userem_do_kasacji.Add(document);
                }

            }

            if (deployments_z_userem_do_kasacji.Count == 0)
            {
                var user_do_kasacji = _Userservice.Get(UserId);

                var user_list = new List<User>();

               user_list.Add(user_do_kasacji);

                if (user_do_kasacji == null)
                    return NotFound();

                var viewModel = new ShowAllUserViewModel()
                {
                    Users = user_list
                };

                _Userservice.Remove(user_do_kasacji.UserId);

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("NotDelUser", "User", new { UserId });
            }
        }

        [HttpGet("NotDelUser")]
        public IActionResult NotDelUser(string UserId)
        {
            var all_deployments_list = new List<Deployment>(_DeploymentService.Get());

            var deployments_z_userem_do_kasacji = new List<Deployment>();

            foreach (var document in all_deployments_list)
            {
                if (document.SchemaCreatedByUserId == UserId)
                {
                    deployments_z_userem_do_kasacji.Add(document);
                }

            }

            var document_list_toDisplay = new List<DeploymentToDisplay>();

            foreach (var document in deployments_z_userem_do_kasacji)
            {
                var document_toDisplay = new DeploymentToDisplay(document, _ProjectService, _DatabaseService, _Userservice);

                document_list_toDisplay.Add(document_toDisplay);
            }

            var viewModel = new ShowAllDeploymentViewModel()
            {
                DeploymentToDisplay_List = document_list_toDisplay
            };

            return View(viewModel);
        }



        [HttpGet("ShowUser")]
        public IActionResult ShowUser()
        {
            var lista_userow = new List<SelectListItem>();

            var user_list = new List<User>(_Userservice.Get());

            foreach (var document in user_list)
            {
                lista_userow.Add(new SelectListItem { Selected = false, Text = document.FullName, Value = document.UserId });
            }
            var slist_user = new SelectList(lista_userow, "Value", "Text");

            var viewModel = new ShowUserViewModel()
            {
                
                SUserlist = slist_user
            };

            return View(viewModel);
        }
    }
}
