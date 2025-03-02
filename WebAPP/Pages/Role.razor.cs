﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WebAPP.Interfaces;
using WebAPP.Models.Dtos;
using WebAPP.Services;

namespace WebAPP.Pages
{
    public partial class Role : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        public IEnumerable<RoleDto> Roles { get; set; } = new List<RoleDto>();

        private ClaimsPrincipal user;
        private AuthenticationState authState;
        private string searchString = "";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Roles = await UserService.GetRoles();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving roles: {ex.Message}");
            }

            authState = await AuthStateProvider.GetAuthenticationStateAsync();
            user = authState.User;
        }

        private IEnumerable<RoleDto> FilteredRoles => string.IsNullOrWhiteSpace(searchString)
        ? Roles
        : Roles.Where(e => e.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

        private void UpdateFilteredRoles(ChangeEventArgs e)
        {
            searchString = e.Value?.ToString();
        }

        private async Task AddRole()
        {
            await Task.Run(() => NavManager.NavigateTo("/addrole"));
        }

        private async Task EditRole(int Id)
        {
            await Task.Run(() => NavManager.NavigateTo($"/editrole/{Id}"));
        }
    }
}