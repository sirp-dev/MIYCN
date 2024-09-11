using Application.Commands.TrainingCommand;
using Application.Commands.TimeTableCommand;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Queries.TrainingFacilitatorQueries;
using Application.Queries.ModuleTopicQueries;
using Microsoft.AspNetCore.Mvc.Rendering;
using Application.Queries.TrainingQueries;

namespace RazorWebUI.Areas.ITrainings.Pages.TimetablePage
{
    public class AddModel : PageModel
    {
        private readonly IMediator _mediator;

        public AddModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public TimeTable TimeTable { get; set; }
        public List<SelectListItem> ModuleTopicDropdownDto { get; set; }
        public List<SelectListItem> FacilitatorDropdownDto { get; set; }
        public Training Training { get; private set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {

            GetByIdTrainingQuery TCommand = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(TCommand);
            if(Training == null)
            {
                return RedirectToPage("/Admin/Index");
            }
            var query = new ListFacilitatorByTrainingIdQuery(id);
            var facilitatorlist = await _mediator.Send(query);

            FacilitatorDropdownDto = facilitatorlist
           .Select(eq => new SelectListItem
           {
               Value = eq.Id.ToString(),
               Text = $"{eq.FullnameX}"
           }).ToList();



            //
            var moduletopics = new ListModuleTopicQuery();
            var listmoduletopics = await _mediator.Send(moduletopics);

            ModuleTopicDropdownDto = listmoduletopics
            .Select(eq => new SelectListItem
            {
                Value = eq.Id.ToString(),
                Text = $"{eq.Title} ({eq.Module.Title})"
            }).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                AddTimeTableCommand Command = new AddTimeTableCommand(TimeTable);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("/TimetablePage/Index", new {id = TimeTable.TrainingId});
            }
            catch (Exception ex)
            {
                return Page();

            }
        }

    }
}
