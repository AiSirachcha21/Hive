using System.Collections.Generic;

namespace Hive.Shared.Login
{
    public class CurrentUser
    {
        public bool IsAuthenticated { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}
