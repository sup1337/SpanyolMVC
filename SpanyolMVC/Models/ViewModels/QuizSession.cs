namespace SpanyolMVC.Models.ViewModels;

public class QuizSession
{
    public DateTime AttemptedAt { get; set; }
    public int Correct { get; set; }
    public int Total { get; set; }
    public List<QuizResultDetails> Details { get; set; }
}