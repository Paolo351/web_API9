using Microsoft.CodeAnalysis;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace web_API9.Models
{
    public class Database
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DatabaseId { get; set; }

        public string Name { get; set; }

        public string Engine { get; set; }

        public Database (string Name_wej, string Engine_wej)
        {
            //Random rnd = new Random();
            //int nProcessID = Process.GetCurrentProcess().Id;
            //short short_n = Convert.ToInt16(nProcessID);
            //var myObjectId = new ObjectId(DateTime.UtcNow, rnd.Next(1, 100), short_n, 1);
            this.DatabaseId = "";
            this.Name = Name_wej;
            this.Engine = Engine_wej;

        }
    }
}
