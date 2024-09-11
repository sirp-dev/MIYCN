using Application.Queries.TrainingQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rotativa.AspNetCore;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    public class DownloadModel : PageModel
    {
        private readonly IMediator _mediator;

        public DownloadModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public TrainingDto Training { get; set; }

        public async Task<IActionResult> OnGet(long id)
        {
            GetByIdTrainingReportQuery Command = new GetByIdTrainingReportQuery(id);
            Training = await _mediator.Send(Command);
            ViewData["TrainingDto"] = Training;
            var model = Training; // Replace with your actual model retrieval logic
            return new ViewAsPdf("Download", model)
            {
                FileName = "TrainingReport.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = new Rotativa.AspNetCore.Options.Margins(10, 10, 10, 10)
            };
        }
    }
}
