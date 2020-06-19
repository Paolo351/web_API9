using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using web_API9.Models;
using web_API9.Models.Application.Database;
using web_API9.Models.Application.Deployment;
using web_API9.Models.Application.Project;
using web_API9.Models.Application.User;
using web_API9.Infrastructure;

namespace web_API9.Controllers
{

    [Route("Database")]
    [ApiController]
    public class DatabaseController : Controller
    {
        private readonly DatabaseService _DatabaseService;

        public DatabaseController(DatabaseService DatabaseService)
        {
            _DatabaseService = DatabaseService;
        }

        
        [HttpGet("ShowAllDatabase")]
        public IActionResult ShowAllDatabase()
        {
            
            var database_list = new List<Database>(_DatabaseService.Get());

            var viewModel = new ShowAllDatabaseViewModel()
            {
                Databases = database_list
            };
            return View(viewModel);
            
        }

        
        [HttpGet("AddDatabase")]
        public IActionResult AddDatabase(string name_input, string engine_input)
        {
            var baza = new Database(name_input, engine_input);
           
            var database_list = new List<Database>();
            database_list.Add(_DatabaseService.Create(baza));
            var viewModel = new ShowAllDatabaseViewModel()
            {
                Databases = database_list
            };
            return View(viewModel);
        }

        

        [HttpGet("DelDatabase")]
        public IActionResult DelDatabase(string numer)
        {
            var database = _DatabaseService.Get(numer);
            var database_list = new List<Database>();
            database_list.Add(database);

            

            if (database == null)
                return NotFound();

            var viewModel = new ShowAllDatabaseViewModel()
            {
                Databases = database_list
            };

            _DatabaseService.Remove(database.DatabaseId);

            return View(viewModel);
        }

        

        [HttpGet("ShowDatabase")]
        public IActionResult ShowDatabase()
        {
            return View();
        }

        
    }
}
