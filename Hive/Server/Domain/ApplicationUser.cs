using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Hive.Server.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<ProjectUser> ProjectUsers { get; set; }
        public IList<OrganizationUser> OrganizationUsers { get; set; }
    }
}
