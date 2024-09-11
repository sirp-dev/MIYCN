using Application.Queries.IdentityQueries;
using Application.Queries.TrainingQueries;
using Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace RazorWebUI.Areas.Identity.Pages.Account
{
    public class LockedModel : PageModel
    {
        private readonly ILogger<LockedModel> _logger;
        private readonly IMediator _mediator;

        public LockedModel(ILogger<LockedModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public BasicProfileDto BasicProfileDto { get; private set; }

        public async Task<ActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new GetUserByIdQuery(userId);
            BasicProfileDto = await _mediator.Send(query);

            return Page();
        }
    }
}
