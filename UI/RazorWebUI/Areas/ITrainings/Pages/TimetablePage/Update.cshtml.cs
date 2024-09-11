using Application.Commands.TrainingCommand;
using Application.Commands.TimeTableCommand;
using Application.Queries.TrainingQueries;
using Application.Queries.TimeTableQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Queries.ModuleTopicQueries;
using Application.Queries.TrainingFacilitatorQueries;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorWebUI.Areas.ITrainings.Pages.TimetablePage
{
    public class UpdateModel : PageModel
    {
        private readonly IMediator _mediator;

        public UpdateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public TimeTable TimeTable { get; set; }
        public List<SelectListItem> ModuleTopicDropdownDto { get; set; }
        public List<SelectListItem> FacilitatorDropdownDto { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdTimeTableQuery Command = new GetByIdTimeTableQuery(id);
            TimeTable = await _mediator.Send(Command);

            var query = new ListFacilitatorByTrainingIdQuery(id);
            var facilitatorlist = await _mediator.Send(query);

            FacilitatorDropdownDto = facilitatorlist
           .Select(eq => new SelectListItem
           {
               Value = eq.Title.ToString(),
               Text = $"{eq.Id} ({eq.FullnameX})"
           }).ToList();



            //
            var moduletopics = new ListModuleTopicQuery();
            var listmoduletopics = await _mediator.Send(moduletopics);

            ModuleTopicDropdownDto = listmoduletopics
            .Select(eq => new SelectListItem
            {
                Value = eq.Title.ToString(),
                Text = $"{eq.Id} ({eq.Module.Title})"
            }).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                UpdateTimeTableCommand Command = new UpdateTimeTableCommand(TimeTable);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("/TimetablePage/Index", new { id = TimeTable.TrainingId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
