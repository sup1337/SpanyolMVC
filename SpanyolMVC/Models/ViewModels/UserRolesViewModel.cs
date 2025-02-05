namespace SpanyolMVC.Models.ViewModels;

public class UserRolesViewModel
{
    public Guid UserId { get; set; } // Felhasználó azonosítója
    public string Email { get; set; }  // Felhasználó email címe
    public IList<string> Roles { get; set; } // Felhasználó szerepkörei
}