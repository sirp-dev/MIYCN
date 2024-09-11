using Amazon;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RazorWebUI.Areas.User.Pages.Admin
{
    public class UpdateModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<UpdateModel> _logger; 

        public UpdateModel(SignInManager<AppUser> signInManager,
            ILogger<UpdateModel> logger,
            UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger; 
        }
        [BindProperty]
        public AppUser UserDatas { get; set; }

       
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserDatas = await _userManager.FindByIdAsync(id);

            if (UserDatas == null)
            {
                return NotFound();
            }

            // 
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var userupdate = await _userManager.FindByIdAsync(UserDatas.Id);
            //UserDatas = userupdate;
            try
            {


                userupdate.FirstName = UserDatas.FirstName;
                userupdate.LastName = UserDatas.LastName;
                userupdate.MiddleName = UserDatas.MiddleName;
                userupdate.PhoneNumber = UserDatas.PhoneNumber; 
                 
                    userupdate.UserStatus = UserDatas.UserStatus;
                    userupdate.BankAccount = UserDatas.BankAccount;
                    userupdate.BankName = UserDatas.BankName;
                    userupdate.AccountNumber = UserDatas.AccountNumber;
                

                await _userManager.UpdateAsync(userupdate);




                TempData["success"] = "updated successful";
                return RedirectToPage("./info", new { id = userupdate.Id });
            }
            catch (DbUpdateConcurrencyException)
            {
                TempData["error"] = "Unable to update user";
                return Page();

            }


        }

    }

}
