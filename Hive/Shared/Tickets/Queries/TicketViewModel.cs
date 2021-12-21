﻿using Hive.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Shared.Tickets.Queries
{
    public class TicketViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedUserId { get; set; }
        public string AssignedUserName { get; set; }
        public TicketStatus Status { get; set; }
        public string LastUpdated { get; set; }
    }
}
