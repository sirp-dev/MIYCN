using Application.Commands.IdentityCommand;
using Application.Commands.TrainingFacilitatorCommand;
using Application.Queries.IdentityQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class AddFacilitatorsModel : PageModel
    {
        private readonly IMediator _mediator;

        public AddFacilitatorsModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public TrainingFacilitator TrainingFacilitator { get; set; }

        [BindProperty]
        public string Role { get; set; }

        public Training Training { get; set; }

        public string RX { get; set; }
        [BindProperty]
        public long TrainingId { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);

            //
            IEnumerable<string> roles = new List<string> { "Facilitator", "Staff" };
            GetUserListByRoleListDto getflist = new GetUserListByRoleListDto(roles);
            var listfacilitators = await _mediator.Send(getflist);

            var dropdownlist = listfacilitators
    .Select(x => new DropdownUserDto
    {
        Id = x.Id,
        Name = $"{x.Fullname} - {x.Email} - {x.Phone} - {x.Category}" // Concatenate full name
    })
    .ToList();

            ViewData["UserId"] = new SelectList(dropdownlist, "Id", "Name");

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                AddTrainingFacilitatorCommand regCommand = new AddTrainingFacilitatorCommand(TrainingFacilitator);
                await _mediator.Send(regCommand);

                return RedirectToPage("./Facilitators", new { id = TrainingFacilitator.TrainingId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }

    }
}
