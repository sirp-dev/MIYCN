using Application.Queries.IdentityQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.User.Pages.Account
{
    public class IndexModel : PageModel
    {
       
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public FullProfileDto UserDatas { get; private set; }

        public async Task<ActionResult> OnGetAsync()
        {
           string name = User.Identity.Name;
            var query = new GetUserProfileByNameQuery(name);
            UserDatas = await _mediator.Send(query);

            return Page();
        }
    }
}
