using System;

namespace Hive.Domain
{
    public class OrganizationUser
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public string MemberId { get; set; }
        public ApplicationUser Member { get; set; }
    }
}