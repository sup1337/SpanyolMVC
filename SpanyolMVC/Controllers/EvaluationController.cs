using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpanyolMVC.Repositories;

namespace SpanyolMVC.Controllers;

[Authorize(Roles = "User")]
public class EvaluationController : Controller
{
    private readonly IEvaluationRepository _evaluationRepo;
    private readonly UserManager<IdentityUser> _userManager;

    public EvaluationController(IEvaluationRepository evaluationRepo, 
        UserManager<IdentityUser> userManager)
    {
        _evaluationRepo = evaluationRepo;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userId = Guid.Parse(_userManager.GetUserId(User));
        var evaluation = await _evaluationRepo.GetEvaluationAsync(userId);
        return View(evaluation);
    }

    [HttpGet]
    public async Task<JsonResult> GetChartData()
    {
        var userId = Guid.Parse(_userManager.GetUserId(User));
        var evaluation = await _evaluationRepo.GetEvaluationAsync(userId);
        return Json(evaluation);
    }
}