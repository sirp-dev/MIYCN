using Application.Queries.TrainingParticipantQueries;
using Application.Queries.TrainingQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
      [Microsoft.AspNetCore.Authorization.Authorize]

    public class DisabledParticipantsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public DisabledParticipantsModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<ParticipantInTrainingDTo> Datas { get; private set; }
        public Training Training { get; private set; }

        public int Disabled { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            var query = new ListParticipantByTrainingIdQuery(id);
            var outcome = await _mediator.Send(query);
            Datas = outcome.Where(tp => tp.ParticipantTrainingStatus == EnumStatus.ParticipantTrainingStatus.Disabled).ToList();
            
            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);
            return Page();
        }



    }
}
