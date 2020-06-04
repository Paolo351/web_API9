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
using web_API9.Controllers;

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

            string user_string, project_string, db_string;
            var document_R = new Deployment_R();
            //var _project = new Project();
            Project _project;
            Database _db;
            Userz _user;

            foreach (var document in deployment_list)
            {
                //project = (_ProjectService.Get(document.AttachedToProjectId)).Name;
                //fullname = (_UserzService.Get(document.SchemaCreatedByUserId)).FullName;
                //db = (_DatabaseService.Get(document.TargetDbId)).Name;

                //_project = _ProjectService.Get("5ed757671e77180006856a7c");
                //project_string = _project.Name;
                //_db = _DatabaseService.Get("5ed636562f2bad0006deba6d");
                //db_string = _db.Name;
                //_user = _UserzService.Get("5ed77e731e77180006856a80");
                //user_string = _user.FullName;

                _project = _ProjectService.Get(document.AttachedToProjectId);
                project_string = _project.Name;
                _db = _DatabaseService.Get(document.TargetDbId);
                db_string = _db.Name;
                _user = _UserzService.Get(document.SchemaCreatedByUserId);
                user_string = _user.FullName;

                document_R.DeploymentId = document.DeploymentId;
                document_R.Name = document.Name;
                document_R.DeployMode = document.DeployMode;
                document_R.PlannedTimeOfDeployment = document.PlannedTimeOfDeployment;
                document_R.TimeOfDeployment = document.TimeOfDeployment;
                document_R.Details = document.Details;
                document_R.HasBeenDeployed = document.HasBeenDeployed;
                document_R.AttachedFeatureDescription = document.AttachedFeatureDescription;
                document_R.SchemaContent = document.SchemaContent;
                document_R.TargetDbId = document.TargetDbId;
                document_R.Nazwa_bd = db_string;
                document_R.SchemaCreatedByUserId = document.SchemaCreatedByUserId;
                document_R.Nazwa_user = user_string;
                document_R.AttachedToProjectId = document.AttachedToProjectId;
                document_R.Nazwa_project = project_string;

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
