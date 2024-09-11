using Application.Queries.TrainingQueries;
using Application.Queries.UserTestQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
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
        public List<UserTestListDto> UserTest { get; set; }
        public Training Training { get; private set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            ListUserTestByTrainingQuery Command = new ListUserTestByTrainingQuery(id);
            UserTest = await _mediator.Send(Command);


            GetByIdTrainingQuery TCommand = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(TCommand);
            return Page();
        }
    }
}
