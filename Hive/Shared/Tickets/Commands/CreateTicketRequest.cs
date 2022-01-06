using System;
using System.ComponentModel.DataAnnotations;

namespace Hive.Shared.Tickets.Commands
{
    public class CreateTicketRequest
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedUserId { get; set; }
        [Required]
        public Guid ProjectId { get; set; }
    }
}
