using Application.Queries.ModuleQueries;
using Application.Queries.ModuleTopicQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.ModulePage
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class ModuleReadModel : PageModel
    {
        private readonly IMediator _mediator;

        public ModuleReadModel(IMediator mediator)
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
