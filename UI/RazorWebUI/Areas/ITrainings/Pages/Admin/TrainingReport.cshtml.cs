using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class TrainingReportModel : PageModel
    {


        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public TrainingReportModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public List<Training> Datas { get; private set; }

        public async Task OnGetAsync()
        {

            var query = new ListAllTrainingQuery();
           var xDatas = await _mediator.Send(query);

            Datas = await xDatas.ToListAsync();
        }

    }
}
