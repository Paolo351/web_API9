using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_API9.Models
{
    public class Database
    {
        public ObjectId DatabaseId { get; set; }

        public string Name { get; set; }

        public string Engine { get; set; }
    }
}
