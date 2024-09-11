using Application.Commands.TrainingCommand;
using Application.Commands.GalleryCommand;
using Application.Queries.TrainingQueries;
using Application.Queries.GalleryQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.GalleryPage
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
        public Gallery Gallery { get; set; }
        [BindProperty]
        public IFormFile? imagefile { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdGalleryQuery Command = new GetByIdGalleryQuery(id);
            Gallery = await _mediator.Send(Command);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                UpdateGalleryCommand Command = new UpdateGalleryCommand(Gallery, imagefile);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Index", new { id = Gallery.TrainingId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
