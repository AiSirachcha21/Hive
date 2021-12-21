using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hive.Domain
{
    public class Project : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<ProjectUser> ProjectUsers { get; set; }
        public Guid OrganizationId { get; set; }
        public string ProjectOwnerId { get; set; }
        public ApplicationUser ProjectOwner { get; set; }
    }
}