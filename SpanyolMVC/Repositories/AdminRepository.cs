using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpanyolMVC.Models.ViewModels;

namespace SpanyolMVC.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async Task<List<UserRolesViewModel>> GetAllUsersWithRolesAsync(
        string searchQuery = null,
        string sortBy = null,
        string sortDirection = null,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var usersQuery = _userManager.Users.AsQueryable();

        // Keresés csak az e-mail cím alapján
        if (!string.IsNullOrEmpty(searchQuery))
        {
            usersQuery = usersQuery.Where(u => u.Email.Contains(searchQuery));
        }

        // Rendezés
        if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(sortDirection))
        {
            if (sortDirection.Equals("Asc", StringComparison.OrdinalIgnoreCase))
            {
                usersQuery = usersQuery.OrderBy(u => EF.Property<object>(u, sortBy));
            }
            else if (sortDirection.Equals("Desc", StringComparison.OrdinalIgnoreCase))
            {
                usersQuery = usersQuery.OrderByDescending(u => EF.Property<object>(u, sortBy));
            }
        }

        // Lapozás
        var skipResults = (pageNumber - 1) * pageSize;
        var users = await usersQuery.Skip(skipResults).Take(pageSize).ToListAsync();

        var userRolesViewModel = new List<UserRolesViewModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userRolesViewModel.Add(new UserRolesViewModel
            {
                UserId = Guid.Parse(user.Id),
                Email = user.Email,
                Roles = roles
            });
        }

        return userRolesViewModel;
    }
    public async Task<int> CountAsync(string searchQuery = null)
    {
        var usersQuery = _userManager.Users.AsQueryable();

        // Keresés csak az e-mail cím alapján
        if (!string.IsNullOrEmpty(searchQuery))
        {
            usersQuery = usersQuery.Where(u => u.Email.Contains(searchQuery));
        }

        return await usersQuery.CountAsync();
    }

    public async Task<ManageUserRolesViewModel> GetUserRolesAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return null;
        }

        var roles = await _roleManager.Roles.ToListAsync();
        var userRoles = await _userManager.GetRolesAsync(user);

        var model = new ManageUserRolesViewModel
        {
            UserId = Guid.Parse(user.Id),
            UserEmail = user.Email,
            Roles = roles.Select(r => new RoleViewModel
            {
                RoleId = Guid.Parse(r.Id),
                RoleName = r.Name,
                Selected = userRoles.Contains(r.Name)
            }).ToList()
        };

        return model;
    }

    public async Task<bool> UpdateUserRolesAsync(Guid userId, List<string> roles)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return false;
        }

        var currentRoles = await _userManager.GetRolesAsync(user);
        var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);

        if (!removeResult.Succeeded)
        {
            return false;
        }

        var addResult = await _userManager.AddToRolesAsync(user, roles);

        return addResult.Succeeded;
    }
    
    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return false; // Felhasználó nem található
        }

        // Felhasználó szerepköreinek törlése
        var userRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, userRoles);

        // Felhasználó claim-jeinek törlése
        var userClaims = await _userManager.GetClaimsAsync(user);
        foreach (var claim in userClaims)
        {
            await _userManager.RemoveClaimAsync(user, claim);
        }

        // Felhasználó törlése
        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }
}