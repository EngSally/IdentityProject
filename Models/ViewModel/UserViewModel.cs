namespace IdentityProject.Models.ViewModel
{
    public class UserViewModel
    {
        public string  Id { get; set; }
        public string FristName { get; set; }=string.Empty;
        public string  LastName { get; set; }= string.Empty;
        public string Email { get; set; }= string.Empty;
        public string UserName { get; set; }= string.Empty;
        public IEnumerable<string> Roles { get; set;} = new List<string>();

    }
}
