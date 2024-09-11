using Application.Queries.TrainingQueries;
using Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
      [Microsoft.AspNetCore.Authorization.Authorize]
    public class MainReport2Model : PageModel
    {


        private readonly ILogger<MainReport2Model> _logger;
        private readonly IMediator _mediator;
        public List<IGrouping<long?, TrainingDto>> GroupedData { get; private set; }

        public MainReport2Model(ILogger<MainReport2Model> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public List<TrainingDto> Datas { get; private set; }

        public async Task OnGetAsync()
        {

            var query = new GetTrainingReportQuery();
            Datas = await _mediator.Send(query);

            // Group the data by CategoryId
            GroupedData = Datas.Where(x=>x.CategoryId != null).OrderBy(x=>x.CategoryId).GroupBy(x => x.CategoryId).ToList();
        }

    }
}
