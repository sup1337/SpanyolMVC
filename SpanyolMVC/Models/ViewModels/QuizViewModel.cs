// Models/ViewModels/QuizViewModel.cs
public class QuizViewModel
{
    public Guid Id { get; set; }
    public string Infinitive { get; set; }
    public string Person { get; set; }
    public string Tense { get; set; }
    public string CorrectAnswer { get; set; }
    public List<string> Options { get; set; }
    public bool IsReflexive { get; set; }
    public bool IsIrregular { get; set; }
    public string UserAnswer { get; set; }
    
    public string Translation { get; set; }

}