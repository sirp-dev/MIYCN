using Application.Queries.TrainingFacilitatorQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
      [Microsoft.AspNetCore.Authorization.Authorize]

    public class FacilitatorPrintModel : PageModel
    {
        private readonly ILogger<PrintFacilitatorsModel> _logger;
        private readonly IMediator _mediator;

        public FacilitatorPrintModel(ILogger<PrintFacilitatorsModel> logger, IMediator mediator)
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
