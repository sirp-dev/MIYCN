using Application.Queries.ModuleTopicQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Account
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class ViewModuleModel : PageModel
    {
        private readonly IMediator _mediator;

        public ViewModuleModel(IMediator mediator)
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
    }
}
