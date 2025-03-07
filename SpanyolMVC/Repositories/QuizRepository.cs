using Microsoft.EntityFrameworkCore;
using SpanyolMVC.Data;
using SpanyolMVC.Models.Domain;
using SpanyolMVC.Models.ViewModels;

namespace SpanyolMVC.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly SpanishDbContext _spanishDbContext;

    public QuizRepository(SpanishDbContext spanishDbContext)
    {
        _spanishDbContext = spanishDbContext;
    }

    public async Task<List<Quiz>> GenerateQuizQuestionsAsync(int numberOfQuestions, string person, string tense, bool isIrregular, bool isReflexive, int difficulty)
    {
        var query = _spanishDbContext.Words.AsQueryable();

        // Filter by irregular/regular
        if (isIrregular)
        {
            query = query.Where(w => w.Group >= 100);
        }
        

        // Filter by reflexive verbs
        if (isReflexive)
        {
            query = query.Where(w => w.Infinitive.EndsWith("se"));
        }

        // Filter by difficulty level
        query = query.Where(w => w.Difficulty == difficulty);

        // Ensure we have enough words to generate the quiz
        var totalWords = await query.CountAsync();
        if (totalWords < numberOfQuestions)
        {
            return new List<Quiz>();
        }

        // Random order and pagination
        var words = await query
            .OrderBy(x => Guid.NewGuid()) // Random order compatible with SQL Server
            .Take(numberOfQuestions)
            .ToListAsync();

        // Generate quiz questions
        var quizQuestions = words.Select(w => new Quiz
        {
            Id = w.Id,
            Infinitive = w.Infinitive,
            Person = person,
            Tense = tense,
            CorrectAnswer = GetConjugation(w, person, tense),
            Options = GetOptions(w, person, tense),
            IsReflexive = w.Infinitive.EndsWith("se"),
            IsIrregular = w.Group >= 100
        }).ToList();

        return quizQuestions;
    }
    private string GetConjugation(Words word, string person, string tense)
    {
        return tense switch
        {
            "Present" => person switch
            {
                "1" => word.Present1,
                "2" => word.Present2,
                "3" => word.Present3,
                "4" => word.Present4,
                "5" => word.Present5,
                "6" => word.Present6,
                _ => string.Empty
            },
            "Past" => person switch
            {
                "1" => word.Imperfect1,
                "2" => word.Imperfect2,
                "3" => word.Imperfect3,
                "4" => word.Imperfect4,
                "5" => word.Imperfect5,
                "6" => word.Imperfect6,
                _ => string.Empty
            },
            "Future" => person switch
            {
                "1" => word.Future1,
                "2" => word.Future2,
                "3" => word.Future3,
                "4" => word.Future4,
                "5" => word.Future5,
                "6" => word.Future6,
                _ => string.Empty
            },
            "Conditional" => person switch
            {
                "1" => word.Conditional1,
                "2" => word.Conditional2,
                "3" => word.Conditional3,
                "4" => word.Conditional4,
                "5" => word.Conditional5,
                "6" => word.Conditional6,
                _ => string.Empty
            },
            "SubjunctivePresent" => person switch
            {
                "1" => word.SubjunctivePresent1,
                "2" => word.SubjunctivePresent2,
                "3" => word.SubjunctivePresent3,
                "4" => word.SubjunctivePresent4,
                "5" => word.SubjunctivePresent5,
                "6" => word.SubjunctivePresent6,
                _ => string.Empty
            },
            "SubjunctiveImperfect" => person switch
            {
                "1" => word.SubjunctiveImperfect1,
                "2" => word.SubjunctiveImperfect2,
                "3" => word.SubjunctiveImperfect3,
                "4" => word.SubjunctiveImperfect4,
                "5" => word.SubjunctiveImperfect5,
                "6" => word.SubjunctiveImperfect6,
                _ => string.Empty
            },
            "ImperativePositive" => person switch
            {
                "2" => word.ImperativePositive2,
                "3" => word.ImperativePositive3,
                "4" => word.ImperativePositive4,
                "5" => word.ImperativePositive5,
                "6" => word.ImperativePositive6,
                _ => string.Empty
            },
            "ImperativeNegative" => person switch
            {
                "2" => word.ImperativeNegative2,
                "3" => word.ImperativeNegative3,
                "4" => word.ImperativeNegative4,
                "5" => word.ImperativeNegative5,
                "6" => word.ImperativeNegative6,
                _ => string.Empty
            },
            _ => string.Empty
        };
    }

    private List<string> GetOptions(Words word, string person, string tense)
    {
        var correctAnswer = GetConjugation(word, person, tense);
        var options = new List<string> { correctAnswer };

        // Corrected persons array to use numeric values
        var persons = new[] { "1", "2", "3", "4", "5", "6" };
        var tenses = new[]
        {
            "Present", "Past", "Future", "Conditional", "SubjunctivePresent",
            "SubjunctiveImperfect", "ImperativePositive", "ImperativeNegative"
        };

        var random = new Random();
        while (options.Count < 4)
        {
            var randomPerson = persons[random.Next(persons.Length)];
            var randomTense = tenses[random.Next(tenses.Length)];
            var randomOption = GetConjugation(word, randomPerson, randomTense);

            if (!options.Contains(randomOption) && !string.IsNullOrEmpty(randomOption))
            {
                options.Add(randomOption);
            }
        }

        return options.OrderBy(x => Guid.NewGuid()).ToList();
    }
    public async Task<bool> EvaluateAnswerAsync(Guid wordId, string userAnswer, string person, string tense)
    {
        var word = await _spanishDbContext.Words.FindAsync(wordId);
        if (word == null) return false;

        var correctAnswer = GetConjugation(word, person, tense);
        return string.Equals(correctAnswer, userAnswer, StringComparison.OrdinalIgnoreCase);
    }

    public async Task<Words> GetWordByIdAsync(Guid wordId)
    {
        return await _spanishDbContext.Words.FindAsync(wordId);
    }
}