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
    [Route("Deployment")]
    [ApiController]
    public class DeploymentController : Controller
    {

        private readonly ProjectService _ProjectService;
        private readonly DatabaseService _DatabaseService;
        private readonly DeploymentService _DeploymentService;
        private readonly Userservice _Userservice;

        public DeploymentController(DeploymentService DeploymentService, ProjectService ProjectService, DatabaseService DatabaseService, Userservice Userservice)
        {
            _DeploymentService = DeploymentService;
            _ProjectService = ProjectService;
            _DatabaseService = DatabaseService;
            _Userservice = Userservice;

        }

       
        [HttpGet("AddDeployment")]
        public IActionResult AddDeployment
            (string name_wpis, string deployMode_wpis, DateTime plannedTimeOfDeployment_wpis, DateTime timeOfDeployment_wpis, string details_wpis, Boolean hasBeenDeployed_wpis, 
            string attachedFeatureDescription_wpis, string schemaContent_wpis, string targetDbId_wpis, string schemaCreatedByUserId_wpis, string attachedToProjectId_wpis)
        {
            var document = new Deployment(name_wpis, deployMode_wpis, plannedTimeOfDeployment_wpis, timeOfDeployment_wpis, details_wpis, hasBeenDeployed_wpis,
            attachedFeatureDescription_wpis, schemaContent_wpis, targetDbId_wpis, schemaCreatedByUserId_wpis, attachedToProjectId_wpis);

            var deployment_list = new List<Deployment>();
            deployment_list.Add(_DeploymentService.Create(document));

            var document_toDisplay = new DeploymentToDisplay(document, _ProjectService, _DatabaseService, _Userservice);
            var document_list_toDisplay = new List<DeploymentToDisplay>();
            document_list_toDisplay.Add(document_toDisplay);

            var viewModel = new ShowAllDeploymentViewModel()
            {
                DeploymentToDisplay_List = document_list_toDisplay
            };
            return View(viewModel);
        }


       
        [HttpGet("DelDeployment")]
        public IActionResult DelDeployment(string numer)
        {
            var document = _DeploymentService.Get(numer);
            if (document == null)
                return NotFound();

            var document_toDisplay = new DeploymentToDisplay(document, _ProjectService, _DatabaseService, _Userservice);

            _DeploymentService.Remove(document.DeploymentId);

            var deployment_list = new List<DeploymentToDisplay>();

            deployment_list.Add(document_toDisplay);

            var viewModel = new ShowAllDeploymentViewModel()
            {
                DeploymentToDisplay_List = deployment_list
            };

            return View(viewModel);
        }


        
        [HttpGet("ShowDeployment")]
        public IActionResult ShowDeployment()
        {

            return View();
        }


        
        [HttpGet("ShowAllDeployment")]
        public IActionResult ShowAllDeployment()
        {
            
            var deployment_list = new List<Deployment>(_DeploymentService.Get());
            var document_list_toDisplay = new List<DeploymentToDisplay>();

            

            foreach (var document in deployment_list)
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


       

    }
}
