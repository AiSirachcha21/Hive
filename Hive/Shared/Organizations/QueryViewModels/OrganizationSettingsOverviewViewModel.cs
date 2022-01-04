using Hive.Shared.Projects.Queries;
using System;
using System.Collections.Generic;

namespace Hive.Shared.Organizations.QueryViewModels
{
    public class OrganizationSettingsOverviewViewModel
    {
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public List<ProjectDisplayViewModel> Projects { get; set; }
    }
}
