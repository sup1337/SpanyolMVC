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

    public async Task<IActionResult> Index()
    {
        var usersWithRoles = await _adminRepository.GetAllUsersWithRolesAsync();
        return View(usersWithRoles);
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
}
