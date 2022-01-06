using System;

namespace Hive.Client.Shared.Constants
{
    public static class Routes
    {
        public const string Index = "/";
        public static string IndexOfOrganization(string OrgName) => $"{Index}{OrgName}";
        public const string Login = "/login";
        public const string Register = "/register";
        public const string CreateProject = "/project/create";
        public const string UserOrganizations = "/organizations";
        private const string _userOrganization = "/organization";
        public static string OrganizationSettingsOverview(Guid organizationId)
            => $"{UserOrganizations}/settings/{organizationId}/overview";

        public static string OrganizationSettingsUsers(Guid organizationId)
            => $"{UserOrganizations}/settings/{organizationId}/users";

        public static string OrganizationPage(Guid organizationId)
            => $"{_userOrganization}/{organizationId}";

        public static string ProjectPage(Guid projectId)
           => $"{_userOrganization}/project/{projectId}";

        public static string ProjectBoard(Guid projectId)
            => $"{ProjectPage(projectId)}/board";
    }
}
