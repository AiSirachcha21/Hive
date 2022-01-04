using Hive.Shared.Organizations.QueryViewModels;
using System;

namespace Hive.Client.Shared.Store.OrganizationSettings
{
    public record SetOrganizationSettingsPageDataAction(OrganizationSettingsOverviewViewModel PageData);
    public record FetchOrganizationSettingsPageDataAction(Guid OrganizationId);
    public record UpdateOrganizationNameAction(UpdateOrganizationRequestViewModel data);

}
