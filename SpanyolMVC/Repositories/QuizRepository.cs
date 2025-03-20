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

    public async Task<List<Quiz>> GenerateQuizQuestionsAsync(int numberOfQuestions, string person, string tense,
        bool isIrregular, bool isReflexive, int difficulty)
    {
        var query = _spanishDbContext.Words.AsQueryable();

        if (isReflexive)
        {
            query = query.Where(w => EF.Functions.Like(w.Infinitive, "%se"));
        }

        query = isIrregular
            ? query.Where(w => w.Group >= 100)
            : query.Where(w => w.Group < 100);

        query = query.Where(w => w.Difficulty == difficulty);

        var words = await query.ToListAsync();
        var random = new Random();
        var randomWords = words.OrderBy(x => random.Next()).Take(numberOfQuestions).ToList();

        var quizQuestions = new List<Quiz>();
        var tensePersons = GetValidPersonsForTense(tense); // Új segédfüggvény

        foreach (var word in randomWords)
        {
            var selectedPerson = person == "Random"
                ? GetRandomValidPerson(tensePersons, random)
                : ValidatePerson(person, tensePersons);

            var correctAnswer = GetConjugation(word, selectedPerson, tense);

            // Biztosítjuk, hogy létező konjugációt választunk
            while (string.IsNullOrEmpty(correctAnswer))
            {
                selectedPerson = GetRandomValidPerson(tensePersons, random);
                correctAnswer = GetConjugation(word, selectedPerson, tense);
            }

            quizQuestions.Add(new Quiz
            {
                Id = word.Id,
                Infinitive = word.Infinitive,
                Person = selectedPerson,
                Tense = tense,
                CorrectAnswer = correctAnswer,
                Options = GetOptions(word, selectedPerson, tense),
                IsReflexive = word.Infinitive.EndsWith("se", StringComparison.OrdinalIgnoreCase),
                IsIrregular = word.Group >= 100
            });
        }

        return quizQuestions;
    }

    private List<string> GetValidPersonsForTense(string tense)
    {
        return tense switch
        {
            "ImperativePositive" => new List<string> { "2", "3", "4", "5", "6" },
            "ImperativeNegative" => new List<string> { "2", "3", "4", "5", "6" },
            _ => new List<string> { "1", "2", "3", "4", "5", "6" }
        };
    }

    private string GetRandomValidPerson(List<string> validPersons, Random random)
    {
        return validPersons[random.Next(validPersons.Count)];
    }

    private string ValidatePerson(string requestedPerson, List<string> validPersons)
    {
        return validPersons.Contains(requestedPerson)
            ? requestedPerson
            : validPersons.First(); // Visszatérés első érvényes személlyel
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

        var validPersons = GetValidPersonsForTense(tense);
        var random = new Random();

        while (options.Count < 6)// 6 opciónál több nem kell
        {
            var randomPerson = validPersons[random.Next(validPersons.Count)];
            var randomOption = GetConjugation(word, randomPerson, tense);

            if (!string.IsNullOrEmpty(randomOption) && !options.Contains(randomOption))
            {
                options.Add(randomOption);
            }
        }

        return options.OrderBy(x => random.Next()).ToList();
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