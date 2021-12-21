namespace Hive.Client.Shared.Constants
{
    public static class Routes
    {
        public const string Index = "/";
        public static string IndexOfOrganization(string OrgName) => $"{Index}{OrgName}";
        public const string Login = "/login";
        public const string Register = "/register";
        public const string CreateProject = "/project/create";
    }
}
