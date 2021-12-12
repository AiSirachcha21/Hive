using System;

namespace Hive.Client.Services.Common
{
    public static class ApiRoutes
    {
        // Authentication
        private static readonly string AuthBaseUrl = "api/auth";
        public static readonly string GetCurrentUser = $"{AuthBaseUrl}/getcurrentuserinfo";
        public static readonly string Login = $"{AuthBaseUrl}/login";
        public static readonly string Register = $"{AuthBaseUrl}/register";
        public static readonly string Logout = $"{AuthBaseUrl}/logout";

        // Organization
        private static readonly string OrganizationBaseUrl = "api/Organization";
        public static readonly string CreateOrganization = $"{OrganizationBaseUrl}";
        public static readonly string GetOrganizations = $"{OrganizationBaseUrl}";

        // Project
        private static readonly string ProjectBaseUrl = "api/Project";
        public static string GetProject(Guid organizationId) => $"{ProjectBaseUrl}/{organizationId}";
        public static readonly string CreateProject = $"{ProjectBaseUrl}";

    }
}
