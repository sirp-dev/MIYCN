using Application.Commands.TrainingCommand;
using Application.Commands.SponsorCommand;
using Application.Queries.TrainingQueries;
using Application.Queries.SponsorQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.SponsorPage
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class UpdateModel : PageModel
    {
        private readonly IMediator _mediator;

        public UpdateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public Sponsor Sponsor { get; set; }
        [BindProperty]
        public IFormFile? imagefile { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdSponsorQuery Command = new GetByIdSponsorQuery(id);
            Sponsor = await _mediator.Send(Command);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                UpdateSponsorCommand Command = new UpdateSponsorCommand(Sponsor, imagefile);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Index", new { id = Sponsor.TrainingId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
