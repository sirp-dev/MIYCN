using Application.Queries.TimeTableQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Account
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class UserTimetableModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;
        public Training Training { get; private set; }

        public UserTimetableModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<TimeTable> Datas { get; private set; }
        [BindProperty]
        public long Id { get; set; }

        [BindProperty]
        public long TrainingId { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            Id = id;

            GetByIdTrainingQuery TCommand = new GetByIdTrainingQuery(id);
             Training = await _mediator.Send(TCommand);
            if (Training == null)
            {
                return RedirectToPage("/Admin/Index");
            }
            var query = new ListTimeTableQuery(id);
            Datas = await _mediator.Send(query);

            return Page();
        }
    }
}
