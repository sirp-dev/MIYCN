using Application.Commands.IdentityCommand;
using Application.Queries.IdentityQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class AddUserToTrainingModel : PageModel
    {
        private readonly IMediator _mediator;

        public AddUserToTrainingModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public RegisterDto RegisterDto { get; set; }

        [BindProperty]
        public string Role { get; set; }

        public Training Training { get; set; }

        public string RX { get; set; }
        [BindProperty]
        public long TrainingId { get; set; }

        [BindProperty]
        public IFormFile ExcelFile { get; set; }
        [BindProperty]
        public string? ExistingUserId { get; set; }

        public async Task<IActionResult> OnGetAsync(long id, string rx)
        {
            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);
            RX = rx;

            //
            GetUserListByRoleDto getflist = new GetUserListByRoleDto("Participant");
            var listfacilitators = await _mediator.Send(getflist);

            var dropdownlist = listfacilitators
    .Select(x => new DropdownUserDto
    {
        Id = x.Id,
        Name = $"{x.Fullname} - {x.Email} - {x.Phone}" // Concatenate full name
    })
    .ToList();

            ViewData["UserId"] = new SelectList(dropdownlist, "Id", "Name");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                AddUserByTrainingIdCommand regCommand = new AddUserByTrainingIdCommand(RegisterDto, Role, TrainingId, ExistingUserId);
                var response = await _mediator.Send(regCommand);

                return RedirectToPage("/Admin/RegistrationStatus", new { id = response.UserId, rx = response.Role, txid = response.TrainingId, area = "User" });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }

        public async Task<IActionResult> OnPostUploadAsync()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                UploadParticipantsCommand Command = new UploadParticipantsCommand(ExcelFile, TrainingId);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Result");
            }
            catch (Exception ex)
            {

                GetByIdTrainingQuery Command = new GetByIdTrainingQuery(TrainingId);
                Training = await _mediator.Send(Command);
                RX = RX;

                //
                GetUserListByRoleDto getflist = new GetUserListByRoleDto("Participant");
                var listfacilitators = await _mediator.Send(getflist);

                var dropdownlist = listfacilitators
        .Select(x => new DropdownUserDto
        {
            Id = x.Id,
            Name = $"{x.Fullname} - {x.Email} - {x.Phone}" // Concatenate full name
        })
        .ToList();

                ViewData["UserId"] = new SelectList(dropdownlist, "Id", "Name");
                return Page();

            }
        }


    }

}

