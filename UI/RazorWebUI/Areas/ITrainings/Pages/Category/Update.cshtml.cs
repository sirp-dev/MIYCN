using Application.Commands.TrainingCategoryCommand;
using Application.Queries.TrainingCategoryQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainingCategorys.Pages.Category
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
        public TrainingCategory TrainingCategory { get; set; }
        [BindProperty]
        public IFormFile? leftsignaturefile { get; set; }
        [BindProperty]
        public IFormFile? rightsignaturefile { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdTrainingCategoryQuery Command = new GetByIdTrainingCategoryQuery(id);
            TrainingCategory = await _mediator.Send(Command);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                UpdateTrainingCategoryCommand Command = new UpdateTrainingCategoryCommand(TrainingCategory);
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