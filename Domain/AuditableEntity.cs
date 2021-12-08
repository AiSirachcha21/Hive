using System;

namespace Hive.Domain
{
    public class AuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModfied { get; set; }
    }
}
