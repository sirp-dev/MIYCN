using Application.Queries.TrainingQueries;
using Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class ReportModel : PageModel
    {
        private readonly IMediator _mediator;

        public ReportModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public TrainingDto Training { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdTrainingReportQuery Command = new GetByIdTrainingReportQuery(id);
            Training = await _mediator.Send(Command);
            
            return Page();
        }
    }
}
