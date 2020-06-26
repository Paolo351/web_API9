using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using web_API9.Infrastructure;


namespace web_API9.Models.Application.Project
{
    
    public class ProjectList
    {
        private readonly ProjectService _ProjectService;

        public List<Project> lista { get; set; }

        public ProjectList(ProjectService ProjectService)
        {
            _ProjectService = ProjectService;

            this.lista = _ProjectService.Get();
        }

        

    }
}
