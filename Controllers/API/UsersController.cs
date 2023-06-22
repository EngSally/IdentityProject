using IdentityProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProject.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<CustomIdentityUser> _userManager;

        public UsersController(UserManager<CustomIdentityUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user= await _userManager.FindByIdAsync(userId);
            if (user == null) { return NotFound("NotFound"); }
            var result=await _userManager.DeleteAsync(user);
            if (!result.Succeeded) { throw new Exception(); }
            return Ok("Succeeded");

        }
    }
}
