using Application.Commands.DialyActivityCommand;
using Application.Commands.TrainingFacilitatorCommand;
using Application.Queries.DialyActivityQueries;
using Application.Queries.TrainingFacilitatorQueries;
using Application.Queries.TrainingQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class ActivitiesModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public ActivitiesModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<DialyActivity> Datas { get; private set; }
        public Training Training { get; private set; }
        [BindProperty]
        public DialyActivity DialyActivity { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            var query = new ListDialyActivityByTrainingQuery(id);
            Datas = await _mediator.Send(query);

            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

           

            try
            {
                AddDialyActivityCommand regCommand = new AddDialyActivityCommand(DialyActivity);
                await _mediator.Send(regCommand);
                TempData["success"] = "Success";
                return RedirectToPage("./Activities", new { id = DialyActivity.TrainingId });
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error";
                return RedirectToPage("./Activities", new { id = DialyActivity.TrainingId });

            }
        }

    }
}
