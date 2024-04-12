using COMP2139_Labs.Areas.ProjectManagement.Models;
using COMP2139_Labs.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Labs.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]

    public class UserRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        private async Task<List<string>> GetStringsAsync(ApplicationUser user) 
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRoleViewModel>();
            foreach (ApplicationUser user in users) {
                var thisViewModel = new UserRoleViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.FirstName= user.FirstName;
                thisViewModel.LastName= user.LastName;
                thisViewModel.Email = user.Email;
                thisViewModel.Roles = await GetUserRolesAsync(user);
                userRolesViewModel.Add(thisViewModel);
            
            }
            return View(userRolesViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.UserId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                ViewBag.ErrorMessage = $"User with id={userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesViewModel>();
            foreach(var role in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId= role.Id,
                    RoleName=role.Name
                };
                if(await _userManager.IsInRoleAsync(user,role.Name))
                {
                    userRolesViewModel.Seleted=true;
                }
                else
                {
                    userRolesViewModel.Seleted = false;

                }
                model.Add(userRolesViewModel);
            }
            return View(model);
        }

        private async Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel>model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            { 
                return View(); 
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result= await _userManager.RemoveFromRolesAsync( user, roles);
            if(!result.Succeeded) {
                ModelState.AddModelError("", "cannot remove user from roles");
                return View(model);
            }
           result= await _userManager
                .AddToRolesAsync(user, model.Where(x => x.Seleted).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add users to roles");
                return View(model);
                
            }
            return RedirectToAction("Index");
        }
    }
}
