using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_API9.Models.Application.Deployment
{
    public class Deployment
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

        public string SchemaCreatedByUserId { get; set; }

        public string AttachedToProjectId { get; set; }

        public Deployment(string name_wpis, string deployMode_wpis, DateTime plannedTimeOfDeployment_wpis, DateTime timeOfDeployment_wpis, string details_wpis, Boolean hasBeenDeployed_wpis,
            string attachedFeatureDescription_wpis, string schemaContent_wpis, string targetDbId_wpis, string schemaCreatedByUserId_wpis, string attachedToProjectId_wpis)
        {
            this.DeploymentId = "";
            this.Name = name_wpis;
            this.DeployMode = deployMode_wpis;
            this.PlannedTimeOfDeployment = plannedTimeOfDeployment_wpis;
            this.TimeOfDeployment = timeOfDeployment_wpis;
            this.Details = details_wpis;
            this.HasBeenDeployed = hasBeenDeployed_wpis;
            this.AttachedFeatureDescription = attachedFeatureDescription_wpis;
            this.SchemaContent = schemaContent_wpis;
            this.TargetDbId = targetDbId_wpis;
            this.SchemaCreatedByUserId = schemaCreatedByUserId_wpis;
            this.AttachedToProjectId = attachedToProjectId_wpis;
        }
    }
}
