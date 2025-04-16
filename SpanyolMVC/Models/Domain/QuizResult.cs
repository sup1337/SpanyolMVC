namespace SpanyolMVC.Models.Domain;

public class QuizResult
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime AttemptedAt { get; set; }
        
    // Question details
    public Guid WordId { get; set; }
    public string Person { get; set; }
    public string Tense { get; set; }
    public string CorrectAnswer { get; set; }
    public string UserAnswer { get; set; }
    
    public bool IsIrregular { get; set; }
    
    public bool IsReflexive { get; set; }
    
    public Words Word { get; set; } //  Ez a kulcs
    
    
    public bool IsCorrect => UserAnswer == CorrectAnswer;
    
  
}