using Application.Queries.DialyActivityQueries;
using Application.Queries.DialyUserEvaluationQueries;
using Application.Queries.TrainingQueries;
using Application.Queries.UserTestQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class EvaluationModel : PageModel
    {
        private readonly IMediator _mediator;

        public EvaluationModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public List<DialyUserEvaluation> DialyUserEvaluation { get; set; }
        public Training Training { get; private set; }
        public DialyActivity DialyActivity { get; set; }

        public async Task<IActionResult> OnGetAsync(long id, long aid)
        {
            if (id < 0)
            {
                return NotFound();
            }
           

            var query = new GetByIdDialyActivityQuery(aid);
            DialyActivity = await _mediator.Send(query);

            ListDialyUserEvaluationQuery Command = new ListDialyUserEvaluationQuery(aid);
            DialyUserEvaluation = await _mediator.Send(Command);

            DialyUserEvaluation = DialyUserEvaluation.Where(x=>x.DialyActivity.Date.Date == DialyActivity.Date.Date).ToList();
            GetByIdTrainingQuery TCommand = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(TCommand);
            return Page();
        }
    }
}
