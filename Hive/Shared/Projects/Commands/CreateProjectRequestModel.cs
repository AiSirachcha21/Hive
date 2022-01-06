using System;

namespace Hive.Shared.Projects.Commands
{
    public class CreateProjectRequestModel
    {
        public CreateProjectRequestModel()
        {
        }

        public CreateProjectRequestModel(Guid organizationId, string name, string description, string projectOwnerId)
        {
            OrganizationId = organizationId;
            Name = name;
            Description = description;
            ProjectOwnerId = projectOwnerId;
        }

        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectOwnerId { get; set; }
    }
}
