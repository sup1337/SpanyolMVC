using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpanyolMVC.Data;
using SpanyolMVC.Models;

namespace SpanyolMVC.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SpanishDbContext _context;

    public HomeController(ILogger<HomeController> logger, SpanishDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var words = await _context.Words
            .OrderBy(w => w.Difficulty)
            .Take(10) // Csak példa, 10 elemet jelenít meg
            .ToListAsync();
        return View(words);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    
    public async Task<IActionResult> StudyVerbs(int? difficulty, int? group)
    {
        var verbs = await _context.Words
            .Where(w => (!difficulty.HasValue || w.Difficulty == difficulty) &&
                        (!group.HasValue || w.Group == group))
            .ToListAsync();
    
        return Json(verbs);
    }
    
   

    public async Task<IActionResult> Vocabulary(int? difficulty, int? group, string language = "english", int page = 1, int pageSize = 6)
    {
        var query = _context.Words.AsQueryable();

        if (difficulty.HasValue)
        {
            query = query.Where(w => w.Difficulty == difficulty);
        }

        if (group.HasValue)
        {
            query = query.Where(w => w.Group == group);
        }

        var totalItems = await query.CountAsync();
        var words = await query
            .OrderBy(w => w.Infinitive)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        ViewBag.SelectedDifficulty = difficulty;
        ViewBag.SelectedGroup = group;
        ViewBag.SelectedLanguage = language.ToLower();
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        return View(words);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}