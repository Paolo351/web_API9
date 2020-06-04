using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_API9.Models;

namespace web_API9.Services
{
    public class UserzService
    {
        private readonly IMongoCollection<Userz> _Userzs;

        public UserzService(IBDO_DatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Userzs = database.GetCollection<Userz>(settings.CollectionName_user);
        }


        public List<Userz> Get() =>
            _Userzs.Find(userz => true).ToList();


        public Userz Create(Userz userz)
        {
            _Userzs.InsertOne(userz);
            return userz;
        }

        public Userz Get(string id) =>
           _Userzs.Find<Userz>(userz => userz.UserzId == id).FirstOrDefault();

        public void Remove(string id) =>
            _Userzs.DeleteOne(userz => userz.UserzId == id);

    }
}
