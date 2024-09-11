using Application.Queries.DialyActivityQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class ActivityDetailsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public ActivityDetailsModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public Training Training { get; private set; }
        [BindProperty]
        public DialyActivity DialyActivity { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            var query = new GetByIdDialyActivityQuery(id);
            DialyActivity = await _mediator.Send(query);

            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(DialyActivity.TrainingId);
            Training = await _mediator.Send(Command);
            return Page();
        }
    }
}
