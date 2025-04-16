using SpanyolMVC.Models.ViewModels;

namespace SpanyolMVC.Models.Domain;

public class Evaluation
{
    public string Category { get; set; }
    public string Label { get; set; }
    public int TotalAttempts { get; set; }
    public int CorrectAnswers { get; set; }
    public double Accuracy => TotalAttempts > 0 ? 
        Math.Round((CorrectAnswers / (double)TotalAttempts) * 100, 2) : 0;
    
    public List<QuizSession> QuizHistory { get; set; }
}