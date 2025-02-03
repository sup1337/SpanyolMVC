using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpanyolMVC.Models.Domain;
using SpanyolMVC.Models.ViewModels;
using SpanyolMVC.Repositories;

namespace SpanyolMVC.Controllers;

[Authorize(Roles = "User")]
public class QuizController : Controller
{
    private readonly IWordsRepository _wordsRepository;
    
    public QuizController(IWordsRepository wordsRepository)
    {
        _wordsRepository = wordsRepository;
    }
    
    // Quiz inditasa 
    [HttpGet]
    public async Task<IActionResult> Index(int numberOfQuestions)
    {
        // Ha a numberOfQuestions <= 0, akkor alapértelmezetten 2 legyen
        if (numberOfQuestions <= 0)
        {
            numberOfQuestions = 2;
        }

        var words = await _wordsRepository.GetRandomWordsAsync(numberOfQuestions);
    
        // Ha nincs elég szó, ki kell jelezni valamit
        if (words.Count < numberOfQuestions)
        {
            TempData["ErrorMessage"] = "Not enough words available for the quiz.";
            return RedirectToAction("Create");
        }
        
        var quizQuestions = words.Select(w => new QuizViewModel
        {
            Id = w.Id,
            Hungarian = w.Hungarian,
            English = w.English,
            Infinitive = w.Infinitive
        }).ToList();

        return View(quizQuestions);
    }
   
    public IActionResult Evaluate(List<Quiz> quizAnswers)
    {
        var results = quizAnswers.Select(q => new QuizResultViewModel()
        {
            Hungarian = q.Hungarian,
            English = q.English,
            UserAnswer = q.UserAnswer,
            Infinitive = q.Infinitive,
            IsCorrect = q.IsCorrect
        }).ToList();
        
        return View("Results" ,results);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        if (TempData["ErrorMessage"] != null)
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
        }
        return View();
    }
}