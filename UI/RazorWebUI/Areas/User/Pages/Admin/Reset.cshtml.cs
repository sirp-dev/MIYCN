using Amazon;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.User.Pages.Admin
{
    public class ResetModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public ResetModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public string Password { get; set; }

        [TempData]
        public string StatusMessage { get; set; }



        [BindProperty]
        public AppUser UserData { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            UserData = user;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {


            var user = await _userManager.FindByIdAsync(UserData.Id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            UserData = user;
            var removepass = await _userManager.RemovePasswordAsync(user);
            if (removepass.Succeeded)
            {
                var addpassword = await _userManager.AddPasswordAsync(user, Password);
                if (addpassword.Succeeded)
                {
                    TempData["success"] = "Password updated successful";


                    return RedirectToPage("./Reset", new { id = user.Id });
                }
                else
                {

                    TempData["error"] = "Unable to change Password";
                    return Page();
                }
            }
            else
            {
                TempData["error"] = "Unable to change Password";

                return Page();
            }


        }
    }

}
