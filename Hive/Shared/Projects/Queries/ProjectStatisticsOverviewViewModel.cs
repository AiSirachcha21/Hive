namespace Hive.Shared.Projects.Queries
{
    public class ProjectStatisticsOverviewViewModel
    {
        public int WorkItemsCreated { get; set; }
        public int WorkItemsCompleted { get; set; }
        public decimal AverageHoursPerTicket { get; set; }
    }
}
