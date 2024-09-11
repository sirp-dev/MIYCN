using Application.Commands.SettingCommand;
using Application.Commands.TrainingCommand;
using Application.Queries.SettingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class UpdateSettingModel : PageModel
    {
        private readonly IMediator _mediator;

        public UpdateSettingModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public Setting Setting { get; set; }
        [BindProperty]
        public IFormFile? SignatureLeft { get; set; }
        [BindProperty]
        public IFormFile? SignatureRight { get; set; }

        public async Task<IActionResult> OnGetAsync(long trainingId)
        {
            GetSettingQuery Command = new GetSettingQuery(trainingId);
            Setting = await _mediator.Send(Command);
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                UpdateSettingCommand Command = new UpdateSettingCommand(Setting, SignatureRight, SignatureLeft);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Info", new {id = Setting.TrainingId});
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
