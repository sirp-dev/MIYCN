using Application.Queries.TrainingParticipantQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
      [Microsoft.AspNetCore.Authorization.Authorize]

    public class PrintParticipantsModel : PageModel
    {
        private readonly ILogger<PrintParticipantsModel> _logger;
        private readonly IMediator _mediator;

        public PrintParticipantsModel(ILogger<PrintParticipantsModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<ParticipantInTrainingDTo> Datas { get; private set; }
        public Training Training { get; private set; }
        public async Task<IActionResult> OnGetAsync()
        {

            var query = new ListTrainingParticipantQuery();
            Datas = await _mediator.Send(query);

            return Page();
        }
    }
}
