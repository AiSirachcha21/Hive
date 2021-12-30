using System;
using System.Collections.Generic;

namespace Hive.Shared.Organizations.QueryViewModels
{
    public class OrganizationViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<OrganizationEmployeeViewModel> Employees { get; set; }
        public int ProjectCount { get; set; }
    }
}
