﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Hive.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<ProjectUser> ProjectUsers { get; set; }
        public IList<OrganizationUser> OrganizationUsers { get; set; }
    }
}
