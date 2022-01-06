using Hive.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Shared.Tickets.Commands
{
    public class UpdateTicketRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedUserId { get; set; }
        public TicketStatus Status { get; set; }
    }
}
