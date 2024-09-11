using Application.Queries.SponsorQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.SponsorPage
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

        public IEnumerable<Sponsor> Datas { get; private set; }
        public Training Training { get; private set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {

            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);
            if (Training != null)
            {
                var query = new ListSponsorByTrainingQuery(Training.Id);
                Datas = await _mediator.Send(query);
            }
            else
            {
                return RedirectToPage("/Admin/Index");
            }

            return Page();
        }
    }
}
