using Application.Commands.ProfileCategoryListCommand;
using Application.Queries.ProfileCategoryListQueries;
using Application.Queries.ProfileCategoryQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace RazorWebUI.Areas.User.Pages.Account
{
    public class EditDataModel : PageModel
    {

        private readonly IMediator _mediator;

        public EditDataModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public ProfileCategoryList ProfileCategoryList { get; set; }
        [BindProperty]
        public string LK { get; set; }
        public IList<ProfileCategory> ProfileCategories { get; set; }

        public async Task<IActionResult> OnGetAsync(long id, string lk)
        {
            if (id < 0)
            {
                return NotFound();
            }
            LK = lk;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            GetByIdProfileCategoryListQuery categoryListQuery = new GetByIdProfileCategoryListQuery(id);
            ProfileCategoryList = await _mediator.Send(categoryListQuery);

            GetByUserIdProfileCategoryQuery listcommand = new GetByUserIdProfileCategoryQuery(userId);
            ProfileCategories = await _mediator.Send(listcommand);
            ProfileCategories = ProfileCategories.Where(x => x.Title.ToUpper().Contains(lk)).ToList();


            if (ProfileCategoryList == null)
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

           

            try
            {
                UpdateProfileCategoryListCommand updatecommand = new UpdateProfileCategoryListCommand(ProfileCategoryList);
                await _mediator.Send(updatecommand);

            }
            catch (DbUpdateConcurrencyException)
            {
                
                    throw;
                 
            }
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                return RedirectToPage("./" + LK, new { id = ProfileCategoryList.AppUserId });

            }
            else
            {
                return RedirectToPage("./" + LK);
            }

        }

    }
}
