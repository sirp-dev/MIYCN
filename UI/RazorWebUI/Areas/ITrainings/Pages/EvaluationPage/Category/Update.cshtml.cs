using Application.Commands.TrainingCommand;
using Application.Commands.EvaluationQuestionCategoryCommand;
using Application.Queries.TrainingQueries;
using Application.Queries.EvaluationQuestionCategoryQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.EvaluationPage.Category
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class UpdateModel : PageModel
    {
        private readonly IMediator _mediator;

        public UpdateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public EvaluationQuestionCategory EvaluationQuestionCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdEvaluationQuestionCategoryQuery Command = new GetByIdEvaluationQuestionCategoryQuery(id);
            EvaluationQuestionCategory = await _mediator.Send(Command);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                UpdateEvaluationQuestionCategoryCommand Command = new UpdateEvaluationQuestionCategoryCommand(EvaluationQuestionCategory);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
