using Hive.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hive.Shared.Tickets.Queries
{
    public class TicketViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedUserId { get; set; }
        public string AssignedUserName { get; set; }
        public TicketStatus Status { get; set; }
        public string LastUpdated { get; set; }
    }
}
