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

        [Route("Add_project")]
        public IActionResult Add_project(string name_wpis)
        {
            var projekt = new Project(name_wpis);

            var project_list = new List<Project>();
            project_list.Add(_ProjectService.Create(projekt));
            var viewModel = new Show_all_projectViewModel()
            {
                Projects = project_list
            };
            return View(viewModel);
        }

        [Route("Del_project")]
        public IActionResult Del_project(string numer)
        {
            var project = _ProjectService.Get(numer);
            var project_list = new List<Project>();
            project_list.Add(project);



            if (project == null)
                return NotFound();

            var viewModel = new Show_all_projectViewModel()
            {
                Projects = project_list
            };

            _ProjectService.Remove(project.ProjectId);
            

            return View(viewModel);
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
