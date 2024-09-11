using Application.Queries.IdentityQueries;
using Application.Queries.TrainingQueries;
using Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace RazorWebUI.Areas.ITrainings.Pages.Account
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public List<TrainingByUserDto> TrainingByUserDto { get; private set; }

        public async Task<ActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new ListTrainingsByUserQuery(userId);
            TrainingByUserDto = await _mediator.Send(query);

            return Page();
        }
    }
}
