using System;

namespace Hive.Domain
{
    public class AuditableEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastModfied { get; set; } = DateTime.Now;
    }
}
