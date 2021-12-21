using System;
using System.Collections.Generic;

namespace Hive.Domain
{
    public class Organization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<OrganizationUser> Members { get; set; }
        public IList<Project> Projects { get; set; }
        public string SystemAdminId { get; set; }
        public virtual ApplicationUser SystemAdmin { get; set; }
    }
}
