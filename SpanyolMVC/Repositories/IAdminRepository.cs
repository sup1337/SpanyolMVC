using SpanyolMVC.Models.ViewModels;

namespace SpanyolMVC.Repositories;

public interface IAdminRepository
{
    Task<List<UserRolesViewModel>> GetAllUsersWithRolesAsync(string searchTerm = null);
    Task<ManageUserRolesViewModel> GetUserRolesAsync(Guid userId);
    Task<bool> UpdateUserRolesAsync(Guid userId, List<string> roles);
    Task<bool> DeleteUserAsync(Guid userId); 
}