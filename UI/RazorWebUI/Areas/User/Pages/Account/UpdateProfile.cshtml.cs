using Amazon;
using Application.Commands.DTO;
using Application.Commands.IdentityCommand;
using Application.Commands.TrainingCommand;
using Application.Queries.IdentityQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using Infrastructure.Migrations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.User.Pages.Account
{
    public class UpdateProfileModel : PageModel
    {
        private readonly IMediator _mediator;

        public UpdateProfileModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public FullProfileDto FullProfileDto { get; set; }
        [BindProperty]
        public IFormFile? imagefile { get; set; }
        [BindProperty]
        public IFormFile? imageidcard { get; set; }

        [BindProperty]
        public FullProfileDto Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = User.Identity.Name;
            GetUserProfileByNameQuery Command = new GetUserProfileByNameQuery(user);
            FullProfileDto = await _mediator.Send(Command);
            Input = FullProfileDto;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Input.UpdateProfile = false;
                UpdateProfileCommand Command = new UpdateProfileCommand(Input.Id, Input, imagefile, imageidcard);
                AppResponse response =  await _mediator.Send(Command);
                if(response.Success == true) {
                    TempData["success"] = "Success";
                    return RedirectToPage("./Education");

                    //if(FullProfileDto.UpdateEducation == true)
                    //{

                    //}
                    //else
                    //{
                    //    return RedirectToPage("./Index");

                    //}
                }
                else
                {
                    TempData["error"] = "Unable to validate Profile";
                    return RedirectToPage("./Index");
                }
                
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
