using System;
using System.ComponentModel.DataAnnotations;

namespace Hive.Shared.Tickets.Commands
{
    public class CreateTicketRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssigneeUserId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
