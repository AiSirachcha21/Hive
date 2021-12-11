using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Shared.Projects.Commands
{
    public record CreateProjectRequestModel(Guid OrganizationId, string Name, string Description, string ProjectOwnerId);
}
