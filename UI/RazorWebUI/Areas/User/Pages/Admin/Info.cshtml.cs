using Application.Queries.IdentityQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.User.Pages.Admin
{
    public class InfoModel : PageModel
    {
        private readonly ILogger<InfoModel> _logger;
        private readonly IMediator _mediator;

        public InfoModel(ILogger<InfoModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public FullProfileDto UserDatas { get; private set; }

        public async Task<ActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var query = new GetUserProfileByIdQuery(id);
            UserDatas = await _mediator.Send(query);

            return Page();
        }

    }
}
