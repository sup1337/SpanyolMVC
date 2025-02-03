namespace SpanyolMVC.Models.Domain;

public class Quiz
{
    public Guid Id { get; set; }
    
    public string Hungarian { get; set; }
    
    public string English { get; set; }
    
    public string UserAnswer { get; set; }
    
    public string Infinitive { get; set; }
    
    public bool IsCorrect => UserAnswer?.Trim().Equals(Infinitive, StringComparison.OrdinalIgnoreCase) == true;
}