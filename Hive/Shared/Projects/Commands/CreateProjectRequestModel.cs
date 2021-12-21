using System;

namespace Hive.Shared.Projects.Commands
{
    public record CreateProjectRequestModel(Guid OrganizationId, string Name, string Description, string ProjectOwnerId);
}
