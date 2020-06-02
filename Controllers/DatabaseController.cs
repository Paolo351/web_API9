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

    [Route("Database")]
    [ApiController]
    public class DatabaseController : Controller
    {
        private readonly DatabaseService _DatabaseService;

        public DatabaseController(DatabaseService DatabaseService)
        {
            _DatabaseService = DatabaseService;
        }

        [Route("Show_all_database")]
        public IActionResult Show_all_database()
        {
            
            var database_list = new List<Database>(_DatabaseService.Get());

            var viewModel = new Show_all_databaseViewModel()
            {
                Databases = database_list
            };

            return View(viewModel);
            
        }

        [Route("Show_database")]
        public IActionResult Show_database()
        {
            return View();
        }
    }
}
