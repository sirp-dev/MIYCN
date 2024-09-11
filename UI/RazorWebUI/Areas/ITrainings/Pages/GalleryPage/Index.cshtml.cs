using Application.Commands.DialyEvaluationQuestionCommand;
using Application.Commands.GalleryCommand;
using Application.Queries.GalleryQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.GalleryPage
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<Gallery> Datas { get; private set; }
        public Training Training { get; private set; }

        [BindProperty]
        public long TrainingId { get; set; }

        [BindProperty]
        public long Id { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {

            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);
            if (Training != null)
            {
                var query = new ListGalleryByTrainingQuery(Training.Id);
                Datas = await _mediator.Send(query);
            }
            else
            {
                return RedirectToPage("/Admin/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {


            try
            {
                DeleteGalleryCommand Command = new DeleteGalleryCommand(Id);
                await _mediator.Send(Command);
                 
                return RedirectToPage("./Index", new { id = TrainingId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
