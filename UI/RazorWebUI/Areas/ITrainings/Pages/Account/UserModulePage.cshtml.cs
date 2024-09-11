using Application.Queries.DialyActivityQueries;
using Application.Queries.ModuleQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace RazorWebUI.Areas.ITrainings.Pages.Account
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class UserModulePageModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public UserModulePageModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<Module> Module { get; private set; }
        
        public async Task<IActionResult> OnGetAsync()
        {
             
            var query = new ListModuleQuery();
            Module = await _mediator.Send(query);

            return Page();
        }
    }
}
