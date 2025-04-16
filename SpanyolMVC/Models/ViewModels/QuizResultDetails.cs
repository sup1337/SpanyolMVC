namespace SpanyolMVC.Models.ViewModels;

public class QuizResultDetails
{
    public string Infinitive { get; set; }
    public string Tense { get; set; }
    public string Person { get; set; }
    public string CorrectAnswer { get; set; }
    public string UserAnswer { get; set; }
    public bool IsCorrect { get; set; }
}