namespace IdentityProject.Models.ViewModel
{
    public class UserRolesViewModel
    {
      public   string UserId { get; set; } = string.Empty;
     public    string UserName { get; set; } = string.Empty;
        public  List<RoleSelectedViewModel> Roles { get; set; } 

    }
}
