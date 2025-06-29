using SpanyolMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using SpanyolMVC.Models.Domain;

namespace SpanyolMVC.Repositories
{
    public interface IQuizRepository
    {
        Task<List<Quiz>> GenerateQuizQuestionsAsync(int numberOfQuestions, string person, string tense, bool isIrregular, bool isReflexive, int difficulty,
            string translationLanguage);
        Task<bool> EvaluateAnswerAsync(Guid wordId, string userAnswer, string person, string tense);
        Task<Words> GetWordByIdAsync(Guid wordId);

        Task SaveQuizResultsAsync(List<QuizResultViewModel> results, ClaimsPrincipal user);

    }
}