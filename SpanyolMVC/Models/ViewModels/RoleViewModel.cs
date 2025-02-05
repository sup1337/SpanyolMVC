namespace SpanyolMVC.Models.ViewModels;

public class RoleViewModel
{
    public Guid RoleId { get; set; } // Szerepkör azonosítója
    public string RoleName { get; set; } // Szerepkör neve
    public bool Selected { get; set; } // Ki van-e választva a szerepkör
}