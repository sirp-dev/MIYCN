using Application.Queries.TrainingCategoryQueries;
using Application.Queries.TrainingQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class ReportByCategoryModel : PageModel
    {


        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;
        public TrainingCategory TrainingCategory { get; set; }

        public ReportByCategoryModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public List<TrainingDto> Datas { get; private set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {

            var query = new GetTrainingReportQuery();
            var list = await _mediator.Send(query);
           Datas = list.Where(x=>x.CategoryId == id).ToList();

            var getTrainingCategory = new GetByIdTrainingCategoryQuery(id);
            var result = await _mediator.Send(getTrainingCategory);
            if (result == null)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                TrainingCategory = result;
            }
            return Page();
        }

    }
}
