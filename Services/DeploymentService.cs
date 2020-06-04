using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_API9.Models;

namespace web_API9.Services
{
    public class DeploymentService
    {
        private readonly IMongoCollection<Deployment> _Deployments;

        public DeploymentService(IBDO_DatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Deployments = database.GetCollection<Deployment>(settings.CollectionName_depl);
        }

        public List<Deployment> Get() =>
            _Deployments.Find(deployment => true).ToList();

        public Deployment Get(string id) =>
           _Deployments.Find<Deployment>(deployment => deployment.DeploymentId == id).FirstOrDefault();
    }
}
