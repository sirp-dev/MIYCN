using Application.Commands.ModuleCommand;
using Application.Commands.ModuleTopicCommand;
using Application.Queries.ModuleQueries;
using Application.Queries.ModuleTopicQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.ModulePage
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class UpdateModuleTopicModel : PageModel
    {
        private readonly IMediator _mediator;

        public UpdateModuleTopicModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public ModuleTopic ModuleTopic { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdModuleTopicQuery Command = new GetByIdModuleTopicQuery(id);
            ModuleTopic = await _mediator.Send(Command);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                UpdateModuleTopicCommand Command = new UpdateModuleTopicCommand(ModuleTopic);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Info", new {id = ModuleTopic.ModuleId});
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
