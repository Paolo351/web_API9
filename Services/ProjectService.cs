using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_API9.Models;

namespace web_API9.Services
{
    public class ProjectService
    {
        private readonly IMongoCollection<Project> _Projects;


        public ProjectService(IBDO_DatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Projects = database.GetCollection<Project>(settings.CollectionName_pro);
        }

        public List<Project> Get() =>
            _Projects.Find(project => true).ToList();



    }
}
