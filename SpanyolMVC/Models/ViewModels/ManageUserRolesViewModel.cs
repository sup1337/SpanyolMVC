namespace SpanyolMVC.Models.ViewModels;

public class ManageUserRolesViewModel
{
    public Guid UserId { get; set; } // Felhasználó azonosítója
    public string UserEmail { get; set; } // Felhasználó email címe
    public List<RoleViewModel> Roles { get; set; } // Elérhető szerepkörök listája
}