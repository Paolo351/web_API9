using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_API9.Models.Application.Database;
using web_API9.Models.Application.Deployment;
using web_API9.Models.Application.Project;
using web_API9.Models.Application.User;


namespace web_API9.Infrastructure
{
    public class DeploymentService
    {
        private readonly IMongoCollection<Deployment> _Deployments;

        public DeploymentService(IMongoBDO settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Deployments = database.GetCollection<Deployment>(settings.CollectionName_depl);
        }

        public List<Deployment> Get() =>
            _Deployments.Find(deployment => true).ToList();

        public Deployment Get(string id) =>
           _Deployments.Find<Deployment>(deployment => deployment.DeploymentId == id).FirstOrDefault();

        public void Remove(string id) =>
            _Deployments.DeleteOne(deployment => deployment.DeploymentId == id);

        public Deployment Create(Deployment deployment)
        {
            _Deployments.InsertOne(deployment);
            return deployment;
        }

       
    }
}
