using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_API9.Models
{
    public class Deployment
    {
        public class SchemaUpdate
        {
            
            public string AttachedFeatureDescription { get; set; }

            public string SchemaContent { get; set; }

            public ObjectId TargetDbId { get; set; }

            public ObjectId SchemaCreatedByUserId { get; set; }

            public ObjectId AttachedToProjectId { get; set; }

        }

        public ObjectId _id { get; set; }

        public string Name { get; set; }

        public string DeployMode { get; set; }

        public DateTime PlannedTimeOfDeployment { get; set; }

        public DateTime TimeOfDeployment { get; set; }

        public string Details { get; set; }
        public Boolean HasBeenDeployed { get; set; }

        public SchemaUpdate _SchemaUpdate { get; set; }
    }
}
