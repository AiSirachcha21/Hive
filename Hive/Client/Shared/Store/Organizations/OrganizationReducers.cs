﻿using Fluxor;
using Hive.Client.Shared.Store.Organizations.Actions;
using System.Linq;

namespace Hive.Client.Shared.Store.Organizations
{
    public static class OrganizationReducers
    {
        [ReducerMethod(typeof(GetOrganizationsAction))]
        public static OrganizationState FetchOrganizationData(OrganizationState state)
        {
            return state with
            {
                IsLoading = true,
                Organizations = null
            };
        }

        [ReducerMethod]
        public static OrganizationState ReduceGetOrganizationsResultAction(OrganizationState state, SetOrganizationsAction action) => state with
        {
            IsLoading = false,
            Organizations = action.Organizations
        };

        [ReducerMethod]
        public static OrganizationState UpdateViewedOrganization(OrganizationState state, UpdateViewedOrganizationAction action)
        {
            if (state.Organizations.Count > 0)
            {
                var org = state.Organizations.FirstOrDefault(o => o.Id == action.OrganizationId);
                return state with
                {
                    ViewingOrganization = org
                };
            }

            return state;
        }
    }
}
