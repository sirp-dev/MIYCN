using Application.Commands.TrainingFacilitatorCommand;
using Application.Queries.TrainingFacilitatorQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class UpdateFacilitatorModel : PageModel
    {
        private readonly IMediator _mediator;

        public UpdateFacilitatorModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public TrainingFacilitator TrainingFacilitator { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdTrainingFacilitatorQuery Command = new GetByIdTrainingFacilitatorQuery(id);
            TrainingFacilitator = await _mediator.Send(Command);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                UpdateTrainingFacilitatorStatusCommand Command = new UpdateTrainingFacilitatorStatusCommand(TrainingFacilitator.TrainingId, TrainingFacilitator.Id, TrainingFacilitator.FacilitatorTrainingStatus,
                    TrainingFacilitator.UserId, TrainingFacilitator.Reasons);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Facilitators", new { id = TrainingFacilitator.TrainingId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }

    }
}
