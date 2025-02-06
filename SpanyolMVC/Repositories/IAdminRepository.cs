using SpanyolMVC.Models.ViewModels;

namespace SpanyolMVC.Repositories;

public interface IAdminRepository
{
    Task<List<UserRolesViewModel>> GetAllUsersWithRolesAsync(string searchTerm = null,
        string sortBy = null,
        string sortDirection = null,
        int pageNumber = 1,
        int pageSize = 10);
    Task<ManageUserRolesViewModel> GetUserRolesAsync(Guid userId);
    Task<bool> UpdateUserRolesAsync(Guid userId, List<string> roles);
    Task<bool> DeleteUserAsync(Guid userId); 
    
    Task<int> CountAsync(string searchQuery = null);
    
}