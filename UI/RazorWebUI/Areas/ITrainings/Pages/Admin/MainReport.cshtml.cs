using Application.Queries.TrainingQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
      [Microsoft.AspNetCore.Authorization.Authorize]
    public class MainReportModel : PageModel
    {


        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public MainReportModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public List<TrainingDto> Datas { get; private set; }

        public async Task OnGetAsync()
        {

            var query = new GetTrainingReportQuery();
            Datas = await _mediator.Send(query);
             
        }

    }
}
