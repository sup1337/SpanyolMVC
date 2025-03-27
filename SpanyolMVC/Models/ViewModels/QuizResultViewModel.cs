// Models/ViewModels/QuizResultViewModel.cs
namespace SpanyolMVC.Models.ViewModels;

public class QuizResultViewModel
{
    public Guid WordId { get; set; }
    
    public string Person { get; set; }
    
    public string Tense { get; set; }
    public string Hungarian { get; set; }
    public string English { get; set; }
    public string UserAnswer { get; set; }
    public string Infinitive { get; set; }
    public bool IsCorrect { get; set; }
    public string CorrectAnswer { get; set; }
}