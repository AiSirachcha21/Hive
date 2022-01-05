using Hive.Shared.Projects.Queries;
using System;
using System.Collections.Generic;

namespace Hive.Client.Shared.Store.OrganizationPage
{
    public record SetCurrentOrganizationIdAction(Guid OrganizationId);
    public record FetchOrganizationProjectsAction(Guid OrganizationId);
    public record SetOrganizationProjectsAction(List<ProjectDisplayViewModel> Projects);
}
