using System.Collections.Generic;

namespace Hive.Shared
{
    public class AuthenticatedUser
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}
