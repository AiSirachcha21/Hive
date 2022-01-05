using Hive.Shared.Projects.Queries;
using System;
using System.Collections.Generic;

namespace Hive.Client.Shared.Store.OrganizationPage
{
    public record FetchOrganizationPageAction(Guid OrganizationId);
    public record SetOrganizationProjectsAction(List<ProjectViewModel> Projects);
}
