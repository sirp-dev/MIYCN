using Application.Commands.TrainingCommand;
using Application.Queries.TrainingCategoryQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
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
        public Training Training { get; set; }
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
            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);

            ListTrainingCategoryQuery ListCommand = new ListTrainingCategoryQuery();
           var CategoryTraining = await _mediator.Send(ListCommand);

            ViewData["TrainingCategoryId"] = new SelectList(CategoryTraining, "Id", "Title");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                UpdateTrainingCommand Command = new UpdateTrainingCommand(Training, leftsignaturefile, rightsignaturefile);
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
