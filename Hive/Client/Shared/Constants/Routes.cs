﻿using System;

namespace Hive.Client.Shared.Constants
{
    public static class Routes
    {
        public const string Index = "/";
        public static string IndexOfOrganization(string OrgName) => $"{Index}{OrgName}";
        public const string Login = "/login";
        public const string Register = "/register";
        public const string CreateProject = "/project/create";
        public static string OrganizationSettingsOverview(Guid organizationId)
            => $"/organizations/settings/{organizationId}/overview";

        public static string OrganizationSettingsUsers(Guid organizationId)
            => $"/organizations/settings/{organizationId}/users";

        public static string OrganizationPage(Guid organizationId)
            => $"/organization/{organizationId}";

        public static string ProjectPage(Guid projectId)
           => $"/organization/project/{projectId}";
    }
}
