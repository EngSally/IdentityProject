using IdentityProject.Models;
using IdentityProject.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Mail;

namespace IdentityProject.Controllers
{
    [Authorize(Roles = "Admin")]
   
    public class UsersController : Controller
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
       private readonly RoleManager<IdentityRole > _roleManager;
          
        public UsersController(UserManager<CustomIdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
       
        }

        public async Task<IActionResult> Index()
        {
            var allusers=await _userManager.Users.ToListAsync();
            var users= allusers.Select(u=>new UserViewModel
            {
                Id=u.Id,
                UserName=u.UserName,
                Email=u.Email,
                FristName=u.FirstName,
                LastName=u.LastName,
                Roles=_userManager.GetRolesAsync(u).Result

            }).ToList();


            return View("Index", users);
        }





        [HttpGet]
        public async Task<IActionResult> Add()
        {

            var roles=await  _roleManager.Roles.Select(r=>new RoleSelectedViewModel(){ RoleId=r.Id,RoleName=r.Name}).ToListAsync();
            var model=new UserDetailsFormViewModel()
            {
                Roles = roles
            };
           
            return View("UserFormDetails",  model);

        }

        [HttpPost]
        public async Task<IActionResult> Add(UserDetailsFormViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (!model.Roles.Any(r => r.IsSelected))
            {
                ModelState.AddModelError("Roles", "You Must Select a role");
                return View(model);
            }

            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "Email is exist");
                return View(model);
            }
            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", "UserName is exist");
                return View(model);
            }
            var user = new CustomIdentityUser()
            {
                UserName=model.UserName,
                LastName = model.LastName,
                FirstName = model.FristName,
                Email = model.Email
            };
            
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError("Roles", err.Description);
                }
                return View(model);
              
            }
            await _userManager.AddToRolesAsync(user,model.Roles.Where(r=> r.IsSelected).Select(r=>r.RoleName));

                return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public  async Task<IActionResult> Edit(string userId)
        {
             var user=await  _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();


            UserDetailsFormViewModel model=new UserDetailsFormViewModel ()
            {
                Id=user.Id,
                UserName=user.UserName,
                FristName=user.FirstName,
                LastName=user.LastName,
                Email=user.Email

            };
            var allRoles=await _roleManager.Roles.ToListAsync();
            model.Roles = allRoles.Select(r => new RoleSelectedViewModel()
            {
                RoleId = r.Id,
                RoleName = r.Name,
                IsSelected = _userManager.IsInRoleAsync(user, r.Name).Result
            }).ToList();

            return View("UserFormDetails", model);
        }

        [HttpPost]
        public  async Task<IActionResult> Edit(UserDetailsFormViewModel model)
        {
           if(! ModelState.IsValid) return BadRequest(model);
            var user=await  _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();
            if (!model.Roles.Any(r => r.IsSelected))
            {
                ModelState.AddModelError("Roles", "You Must Select a role");
                return View(model);
            }
            user.FirstName = model.FristName;
            user.LastName=model.LastName;
            user.Email = model.Email;
            user.UserName = model.UserName;
            var result = await _userManager.UpdateAsync(user);
        

            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("Roles", err.Description);
                }
                return View(model);

            }
            await _userManager.ChangePasswordAsync(user, user.PasswordHash, model.Password);
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if (userRoles.Any(r => r == role.RoleName) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);

                if (!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.RoleName);
            }
            return RedirectToAction(nameof(Index));
        }




        [HttpGet]
        public async Task<IActionResult> MangeRole(string  userId)

        {
            var user=await  _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            var allRoles=await   _roleManager.Roles.ToListAsync();
            var model=new UserRolesViewModel(){
            UserId=user.Id,
            UserName = user.UserName,
                Roles=allRoles.Select(role=> new RoleSelectedViewModel{
                    RoleId=role.Id,
                    RoleName=role.Name,
                    IsSelected=_userManager.IsInRoleAsync(user,role.Name).Result
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MangeRole(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
                return NotFound();
            if (!model.Roles.Any(r => r.IsSelected))
            {
                ModelState.AddModelError("Roles", "Select At min One Role");
              return View (model);
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if (userRoles.Any(r => r == role.RoleName) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);

                if (!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.RoleName);
            }

            return RedirectToAction(nameof(Index));
        }

        public     async Task<IActionResult>CheckEmail(UserDetailsFormViewModel model )
        {
            var user =await _userManager.FindByEmailAsync(model.Email);
            bool allow=user is null || user.Id.Equals(model.Id);
            return Json(allow);
        } 
        public     async Task<IActionResult> CheckUsername(UserDetailsFormViewModel model )
        {
            var user =await _userManager.FindByNameAsync(model.UserName);
            bool allow=user is null || user.Id.Equals(model.Id);
            return Json(allow);
        }


       
    }
}
