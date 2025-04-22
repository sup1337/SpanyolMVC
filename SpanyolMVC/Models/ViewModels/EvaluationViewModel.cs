using SpanyolMVC.Models.Domain;

namespace SpanyolMVC.Models.ViewModels;

public class EvaluationViewModel
{
    public List<Evaluation> TenseEvaluations { get; set; }
    public List<Evaluation> PersonEvaluations { get; set; }
    public List<Evaluation> IrregularEvaluations { get; set; }
    public List<Evaluation> ReflexiveEvaluations { get; set; }
    
    public List<QuizSession> QuizHistory { get; set; } 
    
    
    public int TotalAttempts => TenseEvaluations.Sum(e => e.TotalAttempts);
    public int TotalCorrect => TenseEvaluations.Sum(e => e.CorrectAnswers);
    
    public int TotalIncorrect => TotalAttempts - TotalCorrect;
        
    public double CorrectPercentage => TotalAttempts > 0 
        ? Math.Round((TotalCorrect / (double)TotalAttempts) * 100, 2) 
        : 0;
            
    public double IncorrectPercentage => 100 - CorrectPercentage;
    
    public double OverallAccuracy => TotalAttempts > 0 
        ? Math.Round((TotalCorrect / (double)TotalAttempts) * 100, 2) 
        : 0;
}