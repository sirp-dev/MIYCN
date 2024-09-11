using Application.Queries.TrainingQueries;
using Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Account
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class TestModel : PageModel
    {
        private readonly IMediator _mediator;

        public TestModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public TrainingDto Training { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdAndCountTrainingQuery Command = new GetByIdAndCountTrainingQuery(id);
            Training = await _mediator.Send(Command);
            return Page();
        }
    }
}
