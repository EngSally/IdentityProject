
using IdentityProject.Data.Configuration;
using IdentityProject.Filters;
using IdentityProject.Models;
using IdentityProject.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NuGet.Protocol;

namespace IdentityProject.Controllers
{

    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<CustomIdentityUser>  userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }



        public async Task<IActionResult> Index()
        {
            var allRoles=  await  _roleManager.Roles.ToArrayAsync();
            return View(allRoles);
        }
        [HttpGet]
        [AjaxOnly]
        public IActionResult Create()
        {
            return PartialView("_RoleForm");
        }



        [HttpPost]
        public async Task<IActionResult> Create(RoleFromViewModel model)
        {

            if (!ModelState.IsValid) { return Json(ModelState); }
           
          var role=  await _roleManager.CreateAsync(new IdentityRole { Name = model.Name });
            return Ok();
        }


        [HttpGet]
        [AjaxOnly]
        public  async Task <IActionResult> Edit(string  Id)
        {
            var role=await _roleManager.FindByIdAsync(Id);
            if(role == null) { return NotFound(); }
            RoleFromViewModel model=new RoleFromViewModel (){
                Name=role.Name,Id=role.Id
            };
            return PartialView("_RoleForm",model);
        }




        [HttpPost]
        public async Task<IActionResult> Edit(RoleFromViewModel model)
        {

            if (!ModelState.IsValid) { return Json(ModelState); }

           var role= await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ModelState.AddModelError("Name", "This Role Not Found");
                return PartialView("_RoleForm", model);
            }
            role.Name = model.Name;
           await _roleManager.UpdateAsync(role);
            
          
            return Ok();
        }


        [HttpPost]
        //public  async Task<IActionResult> Delete(string Id)
        //{

        //    var role= await _roleManager.FindByIdAsync(Id);

        //        if (role == null)
        //        {
        //            return NotFound();
        //        }
        //    IdentityResult result=new IdentityResult () ;
        //    try
        //    {
               
        //         result = await _roleManager.DeleteAsync(role);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }

        //        foreach (var error in result.Errors)
        //            ModelState.AddModelError("", error.Description);


        //        return Ok(string.Concat(',', result.Errors));
        //    }
        //    catch (Exception ex) {
        //        //return StatusCode.BadRequest()
        //    }
               
        //}
        public async Task <IActionResult> AllowRole(RoleFromViewModel model)
        {
            var role = await  _roleManager.FindByNameAsync(model.Name);
            bool allow=role is null || role.Id.Equals(model.Id);
            return Json(allow);
        }


       


    }
}
