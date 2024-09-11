using Application.Commands.ModuleCommand;
using Application.Commands.ModuleTopicCommand;
using Application.Queries.ModuleQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.ModulePage
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class InfoModel : PageModel
    {
        private readonly IMediator _mediator;

        public InfoModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public Module Module { get; set; }


        [BindProperty]
        public ModuleTopic ModuleTopic { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdModuleQuery Command = new GetByIdModuleQuery(id);
            Module = await _mediator.Send(Command);
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                AddModuleTopicCommand Command = new AddModuleTopicCommand(ModuleTopic);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Info", new { id = ModuleTopic.ModuleId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            try
            {
                DeleteModuleTopicCommand Command = new DeleteModuleTopicCommand(ModuleTopic.Id);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Info", new { id = ModuleTopic.ModuleId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
