using Application.Queries.TrainingParticipantQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.User.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class ListParticipantsModel : PageModel
    {
        private readonly ILogger<ListParticipantsModel> _logger;
        private readonly IMediator _mediator;

        public ListParticipantsModel(ILogger<ListParticipantsModel> logger, IMediator mediator)
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
