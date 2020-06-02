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
            var client = new MongoClient(MongoDB_BDO.ConnectionString_BDO);
            var database = client.GetDatabase(MongoDB_BDO.bd_BDO);
            var collec = database.GetCollection<BsonDocument>(MongoDB_BDO.kolekcja_Deployment);

            _Deployments = database.GetCollection<Deployment>(settings.CollectionName);
        }


        

    }
}
