using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpanyolMVC.Models.ViewModels;
using SpanyolMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpanyolMVC.Controllers
{
    [Authorize(Roles = "User")]
    public class QuizController : Controller
    {
        private readonly IQuizRepository _quizRepository;

        public QuizController(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index(int numberOfQuestions, string person, string tense, bool isIrregular,
            bool isReflexive, int difficulty)
        {
            if (numberOfQuestions <= 0) numberOfQuestions = 2;

            var quizQuestions = await _quizRepository.GenerateQuizQuestionsAsync(
                numberOfQuestions, person, tense, isIrregular, isReflexive, difficulty
            );

            if (quizQuestions.Count < numberOfQuestions)
            {
                TempData["ErrorMessage"] = "Not enough words available for the quiz.";
                return RedirectToAction("Create");
            }

            var quizViewModels = quizQuestions.Select(q => new QuizViewModel
            {
                Id = q.Id,
                Infinitive = q.Infinitive,
                Person = q.Person,
                Tense = q.Tense,
                CorrectAnswer = q.CorrectAnswer,
                Options = q.Options,
                IsReflexive = q.IsReflexive,
                IsIrregular = q.IsIrregular
            }).ToList();

            return View("Index", quizViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> Evaluate(List<QuizViewModel> quizAnswers)
        {
            var results = new List<QuizResultViewModel>();

            foreach (var answer in quizAnswers)
            {
                var word = await _quizRepository.GetWordByIdAsync(answer.Id);
                var isCorrect =
                    await _quizRepository.EvaluateAnswerAsync(answer.Id, answer.UserAnswer, answer.Person,
                        answer.Tense);
                results.Add(new QuizResultViewModel
                {
                    Hungarian = word.Hungarian,
                    English = word.English,
                    UserAnswer = answer.UserAnswer,
                    Infinitive = answer.Infinitive,
                    CorrectAnswer = answer.CorrectAnswer,
                    IsCorrect = isCorrect
                });
            }

            return View("Results", results);
        }
    }
}