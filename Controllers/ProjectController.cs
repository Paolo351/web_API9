using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_API9.Models.Application.Project;
using web_API9.Infrastructure;


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

        
        [HttpGet("AddProject")]
        public IActionResult AddProject(string Name)
        {
            var projekt = new Project(Name);

            var project_list = new List<Project>();
            project_list.Add(_ProjectService.Create(projekt));
            var viewModel = new ShowAllProjectViewModel()
            {
                Projects = project_list
            };
            return View(viewModel);
        }

        
        [HttpGet("DelProject")]
        public IActionResult DelProject(string ProjectId)
        {
            var project = _ProjectService.Get(ProjectId);

            var project_list = new List<Project>();

            project_list.Add(project);



            if (project == null)
                return NotFound();

            var viewModel = new ShowAllProjectViewModel()
            {
                Projects = project_list
            };

            _ProjectService.Remove(project.ProjectId);
            

            return View(viewModel);
        }

        
        [HttpGet("ShowAllProject")]
        public IActionResult ShowAllProject()
        {

            var project_list = new List<Project>(_ProjectService.Get());

            var viewModel = new ShowAllProjectViewModel()
            {
                Projects = project_list
            };

            return View(viewModel);

        }

        

        [HttpGet("ShowProject")]
        public IActionResult ShowProject()
        {

            return View();
        }
    }
}
