using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_API9.Models
{
    public class Deployment_R
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DeploymentId { get; set; }

        public string Name { get; set; }

        public string DeployMode { get; set; }

        public DateTime PlannedTimeOfDeployment { get; set; }

        public DateTime TimeOfDeployment { get; set; }

        public string Details { get; set; }
        public Boolean HasBeenDeployed { get; set; }

        public string AttachedFeatureDescription { get; set; }

        public string SchemaContent { get; set; }

        public string TargetDbId { get; set; }

        public string Nazwa_bd { get; set; }

        public string SchemaCreatedByUserId { get; set; }
        public string Nazwa_user { get; set; }

        public string AttachedToProjectId { get; set; }
        public string Nazwa_project { get; set; }
    }
}
