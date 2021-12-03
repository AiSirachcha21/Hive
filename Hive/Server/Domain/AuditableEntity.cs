using System;

namespace Hive.Server.Domain
{
    public class AuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModfied { get; set; }
    }
}
