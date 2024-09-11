using Application.Commands.IdentityCommand;
using Application.Commands.TrainingParticipantCommand;
using Application.Queries.IdentityQueries;
using Application.Queries.TrainingParticipantQueries;
using Application.Queries.TrainingQueries;
using Azure.Core;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class ParticipantsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;

        public ParticipantsModel(ILogger<IndexModel> logger, IMediator mediator, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _mediator = mediator;
            _userManager = userManager;
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
            Datas = outcome.Where(tp => tp.ParticipantTrainingStatus == EnumStatus.ParticipantTrainingStatus.Active).ToList();
            Disabled = outcome.Where(tp => tp.ParticipantTrainingStatus == EnumStatus.ParticipantTrainingStatus.Disabled).Count();
            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long id)
        {
            var query = new ListParticipantByTrainingIdQuery(id);
            var outcome = await _mediator.Send(query);
            Datas = outcome.Where(tp => tp.ParticipantTrainingStatus == EnumStatus.ParticipantTrainingStatus.Active && tp.SmsSent == false).Take(10).ToList();
            foreach (var item in Datas)
            {
                try
                {
                    var UserDatas = await _userManager.FindByIdAsync(item.Id);
                    if (UserDatas.SmsSent == false)
                    {
                        var xquery = new ResendSmsEmailCommand(item.Id, true, false);
                        var result = await _mediator.Send(xquery);

                        UserDatas.SmsSent = true;
                        await _userManager.UpdateAsync(UserDatas);
                    }
                }
                catch (Exception c) { }

            }

            return RedirectToPage("./Participants", new { id = id });
        }


    }
}
