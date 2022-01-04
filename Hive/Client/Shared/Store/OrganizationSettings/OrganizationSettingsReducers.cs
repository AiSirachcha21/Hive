using Fluxor;

namespace Hive.Client.Shared.Store.OrganizationSettings
{
    public class OrganizationSettingsReducers
    {
        [ReducerMethod(typeof(FetchOrganizationSettingsPageDataAction))]
        public static OrganizationSettingsState FetchOrganizationSettingsPageData(OrganizationSettingsState state) => state with
        {
            IsLoading = true,
        };

        [ReducerMethod]
        public static OrganizationSettingsState SetOrganizationSettingsPageData(OrganizationSettingsState state, SetOrganizationSettingsPageDataAction action) => state with
        {
            IsLoading = false,
            SettingsPageData = action.PageData
        };
    }
}
