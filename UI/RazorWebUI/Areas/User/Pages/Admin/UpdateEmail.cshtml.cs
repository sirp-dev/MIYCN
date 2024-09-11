using Amazon;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace RazorWebUI.Areas.User.Pages.Admin
{
    public class UpdateEmailModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UpdateEmailModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {

            [Required]
            public string NewEmail { get; set; }

        }

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
            var checkemail = await _userManager.FindByEmailAsync(Input.NewEmail);
            if (checkemail == null)
            {
                var emailtoken = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                var cheangeemail = await _userManager.ChangeEmailAsync(user, Input.NewEmail, emailtoken);
                if (cheangeemail.Succeeded)
                {
                    TempData["success"] = "Email updated successful";


                    return RedirectToPage("./info", new { id = user.Id });
                }

                TempData["error"] = "Unable to change Email";
                return Page();
            }
            else
            {
                TempData["error"] = "Email Already Taken";



                return Page();
            }


        }
    }

}
