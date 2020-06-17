using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_API9.Models.Application.User
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public User(string FirstName_wej, string LastName_wej, string PasswordHash_wej, string Email_wej, string Role_wej)
        {
            this.UserId = "";

            this.FirstName = FirstName_wej;
            this.LastName = LastName_wej;
            this.FullName = String.Concat(FirstName, " ", LastName_wej);
            this.PasswordHash = PasswordHash_wej;
            this.Email = Email_wej;
            this.Role = Role_wej;


        }

    }
}
