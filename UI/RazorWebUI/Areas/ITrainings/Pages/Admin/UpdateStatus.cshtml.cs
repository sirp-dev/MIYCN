using Application.Commands.TrainingCommand;
using Application.Commands.TrainingParticipantCommand;
using Application.Queries.TrainingParticipantQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class UpdateStatusModel : PageModel
    {
        private readonly IMediator _mediator;

        public UpdateStatusModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public TrainingParticipant TrainingParticipant { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdTrainingParticipantQuery Command = new GetByIdTrainingParticipantQuery(id);
            TrainingParticipant = await _mediator.Send(Command);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                UpdateTrainingParticipantStatusCommand Command = new UpdateTrainingParticipantStatusCommand(TrainingParticipant.TrainingId, TrainingParticipant.Id, TrainingParticipant.ParticipantTrainingStatus, 
                    TrainingParticipant.UserId, TrainingParticipant.Reasons);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Participants", new {id = TrainingParticipant.TrainingId});
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
