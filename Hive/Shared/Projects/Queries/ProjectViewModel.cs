using System;
using System.Collections.Generic;

namespace Hive.Shared.Projects.Queries
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OrganizationName { get; set; }
        public char NameInitial { get; set; }
        public string Description { get; set; }
        public ProjectStatisticsOverviewViewModel ProjectStatistics { get; set; }
        public List<ProjectUserViewModel> Members { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
