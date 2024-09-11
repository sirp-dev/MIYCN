using Application.Queries.DialyActivityQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace RazorWebUI.Areas.ITrainings.Pages.Account
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class DialyActivityModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public DialyActivityModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<DialyActivity> Datas { get; private set; }
        public Training Training { get; private set; }
        [BindProperty]
        public DialyActivity DialyActivity { get; set; }

        public string UserId { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new ListDialyActivityByTrainingQuery(id);
            Datas = await _mediator.Send(query);

            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);
            return Page();
        }
    }
}
