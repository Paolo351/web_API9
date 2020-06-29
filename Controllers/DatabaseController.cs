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
using web_API9.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult AddDatabase(string Name, string Engine)
        {
            var baza = new Database(Name, Engine);
           
            var database_list = new List<Database>();
            database_list.Add(_DatabaseService.Create(baza));
            var viewModel = new ShowAllDatabaseViewModel()
            {
                Databases = database_list
            };
            return View(viewModel);
        }

        

        [HttpGet("DelDatabase")]
        public IActionResult DelDatabase(string DatabaseId)
        {
            var database = _DatabaseService.Get(DatabaseId);
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
            var lista_baz = new List<SelectListItem>();
            var database_list = new List<Database>(_DatabaseService.Get());
            foreach (var document in database_list)
            {
                lista_baz.Add(new SelectListItem { Selected = false, Text = document.Name, Value = document.DatabaseId });
            }
            var slist_database = new SelectList(lista_baz, "Value", "Text");



            var viewModel = new ShowDatabaseViewModel()
            {

                SDatabaselist = slist_database
            };
            return View(viewModel);
        }

        
    }
}
