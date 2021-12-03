using System;

namespace Hive.Server.Domain
{
    public class ProjectUser
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string MemberId { get; set; }
        public Project Project { get; set; }
        public ApplicationUser Member { get; set; }
    }
}