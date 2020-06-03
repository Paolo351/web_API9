using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web_API9.Models;
using web_API9.Models.ViewModels;
using web_API9.Services;

namespace web_API9.Controllers
{
    [Route("Project")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly ProjectService _ProjectService;

        public ProjectController(ProjectService ProjectService)
        {
            _ProjectService = ProjectService;
        }


        [Route("Show_all_project")]
        public IActionResult Show_all_project()
        {

            var project_list = new List<Project>(_ProjectService.Get());

            var viewModel = new Show_all_projectViewModel()
            {
                Projects = project_list
            };

            return View(viewModel);

        }


        [Route("Show_project")]
        public IActionResult Show_project()
        {

            return View();
        }
    }
}
