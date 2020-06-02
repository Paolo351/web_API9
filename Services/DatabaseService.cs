using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_API9.Models;

namespace web_API9.Services
{
    public class DatabaseService
    {
        private readonly IMongoCollection<Database> _Databases;

        
        public DatabaseService(IBDO_DatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Databases = database.GetCollection<Database>(settings.CollectionName);
        }

        public List<Database> Get() =>
            _Databases.Find(database => true).ToList();

        

    }
}
