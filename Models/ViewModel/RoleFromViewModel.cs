using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace IdentityProject.Models.ViewModel
{
    public class RoleFromViewModel
    {
        public string Id { get; set; }

        [DisplayName(" Role Name")]
        [Required]
        [Remote("AllowRole", null, AdditionalFields = "Id", ErrorMessage = "Role Name Is Exists")]
        public string  Name { get; set; }
    }
}
