using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_API9.Models
{
    public class Userz
    {
        public ObjectId UserzId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public enum Role
        {
            Admin = 0,
            Schema_guard = 1,
            Spectator = 2
        }
    }
}
