using System;
using System.Threading.Tasks;

namespace Hive.Client.Shared.Entities
{
    public class ActiveNavigationItem
    {
        public event Action OnChange;
        public Guid Id { get; set; }
        public string Name { get; set; }

        public async Task Set(ActiveNavigationItem item)
        {
            Id = item.Id;
            Name = item.Name;

            await Task.Run(() => OnChange?.Invoke());
        }
    }
}
