using System;
using System.ComponentModel.DataAnnotations;

namespace Hive.Domain
{
    public class Ticket : AuditableEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid ProjectId { get; set; }
        public string AssignedUserId { get; set; }
        public TicketStatus TicketStatus { get; set; } = TicketStatus.NotStarted;
        public virtual ApplicationUser AssignedUser { get; set; }
        public virtual Project Project { get; set; }
    }
}
