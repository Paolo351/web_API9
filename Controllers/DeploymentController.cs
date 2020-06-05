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
    [Route("Deployment")]
    [ApiController]
    public class DeploymentController : Controller
    {

        private readonly ProjectService _ProjectService;
        private readonly DatabaseService _DatabaseService;
        private readonly DeploymentService _DeploymentService;
        private readonly UserzService _UserzService;

        public DeploymentController(DeploymentService DeploymentService, ProjectService ProjectService, DatabaseService DatabaseService, UserzService UserzService)
        {
            _DeploymentService = DeploymentService;
            _ProjectService = ProjectService;
            _DatabaseService = DatabaseService;
            _UserzService = UserzService;

        }

        [Route("Add_deployment")]
        public IActionResult Add_deployment
            (string name_wpis, string deployMode_wpis, DateTime plannedTimeOfDeployment_wpis, DateTime timeOfDeployment_wpis, string details_wpis, Boolean hasBeenDeployed_wpis, 
            string attachedFeatureDescription_wpis, string schemaContent_wpis, string targetDbId_wpis, string schemaCreatedByUserId_wpis, string attachedToProjectId_wpis)
        {
            var document = new Deployment(name_wpis, deployMode_wpis, plannedTimeOfDeployment_wpis, timeOfDeployment_wpis, details_wpis, hasBeenDeployed_wpis,
            attachedFeatureDescription_wpis, schemaContent_wpis, targetDbId_wpis, schemaCreatedByUserId_wpis, attachedToProjectId_wpis);

            var deployment_list = new List<Deployment>();
            deployment_list.Add(_DeploymentService.Create(document));

            var document_R = new Deployment_R(document, _ProjectService, _DatabaseService, _UserzService);
            var deployment_list_R = new List<Deployment_R>();
            deployment_list_R.Add(document_R);

            var viewModel = new Show_all_deploymentViewModel()
            {
                Deployment_RS = deployment_list_R
            };
            return View(viewModel);
        }


        [Route("Del_deployment")]
        public IActionResult Del_deployment(string numer)
        {
            var document = _DeploymentService.Get(numer);
            if (document == null)
                return NotFound();

            var document_R = new Deployment_R(document, _ProjectService, _DatabaseService, _UserzService);

            _DeploymentService.Remove(document.DeploymentId);

            var deployment_list = new List<Deployment_R>();

            deployment_list.Add(document_R);

            var viewModel = new Show_all_deploymentViewModel()
            {
                Deployment_RS = deployment_list
            };

            return View(viewModel);
        }


        [Route("Show_deployment")]
        public IActionResult Show_deployment()
        {

            return View();
        }

        [Route("Show_all_deployment")]
        public IActionResult Show_all_deployment()
        {
            
            var deployment_list = new List<Deployment>(_DeploymentService.Get());
            var deployment_list_R = new List<Deployment_R>();

            //string user_string, project_string, db_string;
            //var document_R = new Deployment_R();
            
            //Project _project;
            //Database _db;
            //Userz _user;

            foreach (var document in deployment_list)
            {
                var document_R = new Deployment_R(document, _ProjectService, _DatabaseService, _UserzService);


                

                deployment_list_R.Add(document_R);
            }



            var viewModel = new Show_all_deploymentViewModel()
            {
                Deployment_RS = deployment_list_R
            };

            return View(viewModel);

        }


       

    }
}
