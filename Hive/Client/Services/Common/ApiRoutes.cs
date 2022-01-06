using System;

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
        public const string CreateOrganization = _organizationBaseUrl;
        public const string GetOrganizations = _organizationBaseUrl;
        public static string DeleteOrganization(Guid organizationId) => $"{_organizationBaseUrl}?id={organizationId}";
        public static string GetOrganizationSettingsView(Guid organizationId) => $"{_organizationBaseUrl}/GetOrganizationSettingsModel?organizationId={organizationId}";
        public static string CheckForDuplicateOrganization(string name) => $"{_organizationBaseUrl}/CheckDuplicate?name={name}";
        public const string UpdateOrganization = _organizationBaseUrl;
        public static string GetUserPool(Guid organizationId) => _organizationBaseUrl + $"/GetUserPool?organizationId={organizationId}";
        public const string AddToOrganization = $"{_organizationBaseUrl}/AddUserToOrganization";
        public static string GetOrganizationUsers(Guid organizationId) => $"{_organizationBaseUrl}/GetOrganizationUsers?organizationId={organizationId}";

        // Project
        private const string _projectBaseUrl = "api/Project";
        public static string GetProject(Guid projectId) => $"{_projectBaseUrl}/{projectId}";
        public const string CreateProject = $"{_projectBaseUrl}";
        public static string GetUserProjects(Guid organizationId) => $"{_projectBaseUrl}/GetUserProjects?organizationId={organizationId}";
        public const string AddUsersToProject = _projectBaseUrl + "/AddUsers";

        // Tickets
        private const string _ticketBaseUrl = "api/Ticket";
        public static string GetTicketsByProjectId(Guid projectId) => $"{_ticketBaseUrl}/{projectId}";
        public const string CreateTicket = $"{_ticketBaseUrl}/Create";
        public static string DeleteTicket(Guid id) => $"{_ticketBaseUrl}?ticketId={id}";
        public const string UpdateTicket = $"{_ticketBaseUrl}/Update";
    }
}
