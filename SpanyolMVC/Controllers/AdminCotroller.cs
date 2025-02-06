using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpanyolMVC.Models.ViewModels;
using SpanyolMVC.Repositories;

namespace SpanyolMVC.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IAdminRepository _adminRepository;

    public AdminController(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    [HttpGet]
    [ActionName("Index")]
    public async Task<IActionResult> Index(
        string searchQuery = null,
        string sortBy = null,
        string sortDirection = null,
        int pageSize = 10,
        int pageNumber = 1)
    {
        var totalRecords = await _adminRepository.CountAsync(searchQuery);
        var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSize);

        if (pageNumber > totalPages)
        {
            pageNumber--;
        }

        if (pageNumber < 1)
        {
            pageNumber++;
        }

        ViewBag.TotalPages = totalPages;
        ViewBag.SearchQuery = searchQuery;
        ViewBag.SortBy = sortBy;
        ViewBag.SortDirection = sortDirection;
        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;

        var users = await _adminRepository.GetAllUsersWithRolesAsync(searchQuery, sortBy, sortDirection, pageNumber, pageSize);

        return View(users);
    }
    
    [HttpGet]
    public async Task<IActionResult> Search(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return RedirectToAction("Index");
        }

        var users = await _adminRepository.GetAllUsersWithRolesAsync(searchTerm);
        return View("Index", users);
    }

    public async Task<IActionResult> Manage(Guid userId)
    {
        var model = await _adminRepository.GetUserRolesAsync(userId);
        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Manage(ManageUserRolesViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var selectedRoles = model.Roles
            .Where(r => r.Selected)
            .Select(r => r.RoleName)
            .ToList();

        var result = await _adminRepository.UpdateUserRolesAsync(model.UserId, selectedRoles);

        if (!result)
        {
            ModelState.AddModelError("", "Failed to update user roles.");
            return View(model);
        }

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid userId)
    {
        var result = await _adminRepository.DeleteUserAsync(userId);
        if (!result)
        {
            TempData["ErrorMessage"] = "Failed to delete user.(cant delete admin)";
        }
        else
        {
            TempData["SuccessMessage"] = "User deleted successfully.";
        }

        return RedirectToAction("Index");
    }
}
