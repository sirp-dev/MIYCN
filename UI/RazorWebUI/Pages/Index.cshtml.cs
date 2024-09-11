using Application.Queries.AttendanceQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
 
namespace RazorWebUI.Pages
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

        public List<Attendance> Attendances { get; private set; }

        public async Task OnGetAsync()
        {
            var query = new ListAttendanceQuery();
            Attendances = await _mediator.Send(query);
        }

    }
}
