using Microsoft.EntityFrameworkCore;
using SpanyolMVC.Data;
using SpanyolMVC.Models.Domain;
using SpanyolMVC.Models.ViewModels;

namespace SpanyolMVC.Repositories;

public class EvaluationRepository : IEvaluationRepository
{
    private readonly SpanishDbContext _spanishDbContext;

    public EvaluationRepository(SpanishDbContext spanishDbContext)
    {
        _spanishDbContext = spanishDbContext;
    }

    public async Task<EvaluationViewModel> GetEvaluationAsync(Guid userId)
    {
        var results = await _spanishDbContext.QuizResults
            .Include(q => q.Word)
            .Where(q => q.UserId == userId)
            .AsNoTracking()
            .ToListAsync();

        return new EvaluationViewModel
        {
            TenseEvaluations = GetTenseEvaluation(results),
            PersonEvaluations = GetPersonEvaluation(results),
            IrregularEvaluations = GetIrregularEvaluation(results),
            ReflexiveEvaluations = GetReflexiveEvaluation(results)
        };
    }

    private List<Evaluation> GetTenseEvaluation(List<QuizResult> results)
    {
        var tenseTranslations = new Dictionary<string, string>
        {
            {"Present", "Present"},
            {"Past", "Past"},
            {"Future", "Future Tense"},
            {"Conditional", "Conditional Mood"},
            {"SubjunctivePresent", "Subjunctive Present"},
            {"SubjunctiveImperfect", "Subjunctive Past"},
            {"ImperativePositive", "Imperative (Positive)"},
            {"ImperativeNegative", "Imperative (Negative)"}
        };

        return results
            .GroupBy(r => r.Tense)
            .Select(g => new Evaluation
            {
                Category = "Igeidő",
                Label = tenseTranslations.TryGetValue(g.Key, out var translated) 
                    ? translated 
                    : g.Key,
                TotalAttempts = g.Count(),
                CorrectAnswers = g.Count(x => x.UserAnswer == x.CorrectAnswer)
            })
            .OrderBy(e => e.Label)
            .ToList();
    }

    private List<Evaluation> GetPersonEvaluation(List<QuizResult> results)
    {
        return results
            .GroupBy(r => r.Person)
            .Select(g => new Evaluation
            {
                Category = "Person",
                Label = GetPersonLabel(g.Key),
                TotalAttempts = g.Count(),
                CorrectAnswers = g.Count(x => x.UserAnswer == x.CorrectAnswer)
            })
            .OrderBy(e => e.Label)
            .ToList();
    }

    private List<Evaluation> GetIrregularEvaluation(List<QuizResult> results)
    {
        return results
            .GroupBy(r => r.IsIrregular)
            .Select(g => new Evaluation
            {
                Category = "Verb Type",
                Label = g.Key ? "Rendhagyó igék" : "Szabályos igék",
                TotalAttempts = g.Count(),
                CorrectAnswers = g.Count(x => x.UserAnswer == x.CorrectAnswer)
            })
            .ToList();
    }

    private List<Evaluation> GetReflexiveEvaluation(List<QuizResult> results)
    {
        return results
            .GroupBy(r => r.IsReflexive)
            .Select(g => new Evaluation
            {
                Category = "Reflexivitás",
                Label = g.Key ? "Visszaható igék" : "Nem visszaható igék",
                TotalAttempts = g.Count(),
                CorrectAnswers = g.Count(x => x.UserAnswer == x.CorrectAnswer)
            })
            .ToList();
    }

    private string GetPersonLabel(string personNumber)
    {
        return personNumber switch
        {
            "1" => "I (1st singular)",
            "2" => "You (2nd singular)",
            "3" => "He/She/It (3rd singular)",
            "4" => "We (1st plural)",
            "5" => "You (2nd  plural)",
            "6" => "They (3rd plural)",
            _ => "Ismeretlen"
        };
    }
}