﻿using System;

namespace Hive.Client.Services.Common
{
    public static class ApiRoutes
    {
        // Authentication
        private const string _authBaseUrl = "api/auth";
        public const string GetCurrentUser = $"{_authBaseUrl}/getcurrentuserinfo";
        public const string Login = $"{_authBaseUrl}/login";
        public const string Register = $"{_authBaseUrl}/register";
        public const string Logout = $"{_authBaseUrl}/logout";

        // Organization
        private const string _organizationBaseUrl = "api/Organization";
        public const string CreateOrganization = $"{_organizationBaseUrl}";
        public const string GetOrganizations = $"{_organizationBaseUrl}";
        public static string DeleteOrganization(Guid organizationId) => $"{_organizationBaseUrl}?id={organizationId}";
        public static string GetOrganizationSettingsView(Guid organizationId) => $"{_organizationBaseUrl}/GetOrganizationSettingsModel?organizationId={organizationId}";

        // Project
        private const string _projectBaseUrl = "api/Project";
        public static string GetProject(Guid organizationId) => $"{_projectBaseUrl}/{organizationId}";
        public const string CreateProject = $"{_projectBaseUrl}";

    }
}
