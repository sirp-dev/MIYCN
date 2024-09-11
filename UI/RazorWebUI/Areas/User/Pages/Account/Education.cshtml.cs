using Application.Commands.DTO;
using Application.Commands.IdentityCommand;
using Application.Commands.ProfileCategoryListCommand;
using Application.Queries.IdentityQueries;
using Application.Queries.ProfileCategoryQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace RazorWebUI.Areas.User.Pages.Account
{
    public class EducationModel : PageModel
    {
        private readonly IMediator _mediator;

        public EducationModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public BasicProfileDto UserDatas { get; set; }

        public List<ProfileCategory> ProfileCategories { get; set; }

        [BindProperty]
        public ProfileCategoryList ProfileCategoryList { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            GetByUserIdProfileCategoryQuery Command = new GetByUserIdProfileCategoryQuery(userId);
            ProfileCategories = await _mediator.Send(Command);
            ProfileCategories = ProfileCategories.Where(x => x.Title.ToUpper().Contains("EDUCATION")).ToList();
            GetUserByIdQuery getUserByIdQuery = new GetUserByIdQuery(userId);
            UserDatas = await _mediator.Send(getUserByIdQuery);

            return Page();
        }
        public async Task<IActionResult> OnPostCategoriesAsync()
        {
            try
            {
                AddProfileCategoryListCommand command = new AddProfileCategoryListCommand(ProfileCategoryList);
                await _mediator.Send(command);

                GetUserProfileByIdQuery getUserByIdQuery = new GetUserProfileByIdQuery(ProfileCategoryList.AppUserId);
                var updatuser = await _mediator.Send(getUserByIdQuery);

                updatuser.UpdateEducation = false;
                UpdateProfileCommand updateUserCOmmand = new UpdateProfileCommand(updatuser.Id, updatuser, null, null);
                await _mediator.Send(updateUserCOmmand);

                TempData["success"] = "Successfull";
                if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
                {
                    return RedirectToPage("./Education", new { id = updatuser.Id });

                }
                else
                {
                    return RedirectToPage();

                }
            }
            catch (Exception ex)
            {
                return RedirectToPage();
            }
        }

    }
}
