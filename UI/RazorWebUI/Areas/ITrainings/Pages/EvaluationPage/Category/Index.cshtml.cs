using Application.Queries.EvaluationQuestionCategoryQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.EvaluationPage.Category
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<EvaluationQuestionCategory> Datas { get; private set; }

        public async Task OnGetAsync()
        {
            var query = new ListEvaluationQuestionCategoryQuery();
            Datas = await _mediator.Send(query);
        }
    }
}
