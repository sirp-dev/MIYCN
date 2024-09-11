using Application.Queries.TrainingFacilitatorQueries;
using Application.Queries.TrainingQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class FacilitatorsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public FacilitatorsModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public int Disabled {  get; set; }
        public IEnumerable<FacilitatorInTrainingDTo> Datas { get; private set; }
        public Training Training { get; private set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            var query = new ListFacilitatorByTrainingIdQuery(id);
            var outcome = await _mediator.Send(query);
            Disabled = outcome.Where(tp => tp.FacilitatorTrainingStatus == EnumStatus.FacilitatorTrainingStatus.Disabled).Count();
            Datas = outcome.Where(tp => tp.FacilitatorTrainingStatus == EnumStatus.FacilitatorTrainingStatus.Active).ToList();

            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);
            return Page();
        }
    }
}
