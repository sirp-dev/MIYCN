using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RazorWebUI.Pages.Shared.ViewComponents
{
    public class RoleDataViewComponent : ViewComponent
    {

        private readonly RoleManager<AppRole> _roleManager;

        public RoleDataViewComponent(
            RoleManager<AppRole> roleManager
            )
        {

            _roleManager = roleManager;
        }

        public IdentityRole RoleData { get; set; }

        public async Task<IViewComponentResult> InvokeAsync(string name, string Userid, string syle, string fullname)
        {
            RoleData = await _roleManager.FindByNameAsync(name);
            TempData["UserID"] = Userid;
            TempData["style"] = syle;
            TempData["fullname"] = fullname;
            return View(RoleData);
        }
    }

}
