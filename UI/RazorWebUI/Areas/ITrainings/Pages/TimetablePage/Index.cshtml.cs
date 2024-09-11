using Application.Commands.DialyEvaluationQuestionCommand;
using Application.Commands.TimeTableCommand;
using Application.Queries.TimeTableQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.TimetablePage
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<TimeTable> Datas { get; private set; }
        [BindProperty]
        public long Id { get; set; }

        [BindProperty]
        public long TrainingId { get; set; }

        public Training Training { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            Id =id;

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

        public async Task<IActionResult> OnPostDeleteAsync()
        {


            try
            {
                DeleteTimeTableCommand Command = new DeleteTimeTableCommand(Id);
                  await _mediator.Send(Command);
                   TempData["success"] = "Success";

                return RedirectToPage("./Index", new { id = TrainingId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
