using Application.Queries.TrainingFacilitatorQueries;
using Application.Queries.TrainingQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.User.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class AllFacilitatorsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public AllFacilitatorsModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<FacilitatorInTrainingDTo> Datas { get; private set; }
        public Training Training { get; private set; }
        public async Task<IActionResult> OnGetAsync(string name)
        {

            var query = new ListTrainingFacilitatorQuery();
            Datas = await _mediator.Send(query);
            return Page();
        }
    }
}
