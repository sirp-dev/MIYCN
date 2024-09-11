using Application.Queries.AttendanceQueries;
using Application.Queries.IdentityQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.User.Pages.Admin
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

        public IEnumerable<ProfileDto> Datas { get; private set; }

        public async Task OnGetAsync()
        {
            var query = new GetUserListByDto();
            Datas = await _mediator.Send(query);
        }

    }
}
