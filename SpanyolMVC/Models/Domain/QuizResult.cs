namespace SpanyolMVC.Models.Domain;

public class QuizResult
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime AttemptedAt { get; set; }
        
    // Question details
    public Guid WordId { get; set; }
    public string Infinitive { get; set; }
    public string Person { get; set; }
    public string Tense { get; set; }
    public string CorrectAnswer { get; set; }
    public string UserAnswer { get; set; }
        
    // Six answer options
    public string Option1 { get; set; }
    public string Option2 { get; set; }
    public string Option3 { get; set; }
    public string Option4 { get; set; }
    public string Option5 { get; set; }
    public string Option6 { get; set; }
}