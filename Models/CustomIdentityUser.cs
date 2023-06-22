using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdentityProject.Models
{
    public class CustomIdentityUser: IdentityUser
    {
        [DisplayName("First Name")]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        [DisplayName("Last Name")]
        [MaxLength(100)]
        public string LastName { get; set; }=string.Empty;
        public byte[]? ProfilePicture { get; set; } 


    }
}
