using Application.Commands.UserTestCommand;
using Application.Queries.TrainingQueries;
using Application.Queries.TrainingTestQueries;
using Application.Queries.UserTestQueries;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Migrations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using System.Text;

namespace RazorWebUI.Areas.ITrainings.Pages.Account
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class AssessmentModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public AssessmentModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<TrainingTest> Datas { get; private set; }
        public TrainingDto Training { get; set; }
        public string Title { get; set; }
        public string Instruction { get; set; }

        [BindProperty]
        public long TrainingId { get; set; }

        [BindProperty]
        public int Exampath { get; set; }
        public async Task<IActionResult> OnGetAsync(int xty, long tid)
        {
            //
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var checkifwritentest = new CheckUserTestByTrainingCommand(tid, userId, xty);
            var result = await _mediator.Send(checkifwritentest);
            if (result == true)
            {
                return RedirectToPage("./Result", new { xty, tid });
            }

            GetByIdAndCountTrainingQuery Command = new GetByIdAndCountTrainingQuery(tid);
            Training = await _mediator.Send(Command);
            if (Training == null)
            {
                return RedirectToPage("./Index");
            }
            var query = new ListTrainingTestQuery();
            Datas = await _mediator.Send(query);
            if (xty == 0)
            {
                Exampath = 0;
                Title = "PRE TEST";
                Instruction = Training.PreTestInstruction;
                Datas = Datas.Where(x => x.TrainingTestType == EnumStatus.TrainingTestType.PreTest).ToList();
            }
            else if (xty == 2)
            {
                Exampath = 2;
                Title = "POST TEST";
                Instruction = Training.PostTestInstruction;
                Datas = Datas.Where(x => x.TrainingTestType == EnumStatus.TrainingTestType.PostTest).ToList();

            }
            else
            {
                TempData["error"] ="Unable to process test";
                return RedirectToPage("./Info", new {id = tid});
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            StringBuilder formInfo = new StringBuilder();
            var assessmentData = new List<(long questionId, int answer, string userId, long trainingId)>();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach (var key in Request.Form.Keys)
            {
                string value = Request.Form[key];
                formInfo.AppendLine($"{key}: {value}");

                if (key.StartsWith("answer_"))
                {
                    if (long.TryParse(key.Substring("answer_".Length), out long questionId) && int.TryParse(value, out int answer))
                    {
                        assessmentData.Add((questionId, answer, userId, TrainingId));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid data provided.");
                        TempData["response"] = "One or more invalid responses were provided.";
                        return Page();
                    }
                }
            }

            // Now you have assessmentData populated with question IDs and answers
            var command = new UpdateAssestmentCommand(assessmentData);
            await _mediator.Send(command);

            TempData["response"] = "Assessment submitted successfully.";
            return RedirectToPage("./Result", new { xty = Exampath, tid = TrainingId });
        }

    }
}
