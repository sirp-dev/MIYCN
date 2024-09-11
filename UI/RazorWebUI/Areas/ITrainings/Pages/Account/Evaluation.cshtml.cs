using Application.Commands.DialyUserEvaluationCommand;
using Application.Queries.CertificationQueries;
using Application.Queries.DialyActivityQueries;
using Application.Queries.DialyUserEvaluationQueries;
using Application.Queries.TrainingQueries;
using Application.Queries.TrainingTestQueries;
using Application.Queries.UserTestQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text;

namespace RazorWebUI.Areas.ITrainings.Pages.Account
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class EvaluationModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public EvaluationModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<DialyEvaluationQuestion> Datas { get; private set; }
        public Training Training { get; set; }
         
        public DialyActivity DialyActivity { get; set; }
        [BindProperty]
        public long TrainingId { get; set; }

        [BindProperty]
        public long DialyId { get; set; }

        [BindProperty]
        public int Exampath { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            //
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

          var listquestionforactivity = new  ListDialyEvaluationQuestionQuery(id);
            Datas = await _mediator.Send(listquestionforactivity);

            var query = new GetByIdDialyActivityQuery(id);
            DialyActivity = await _mediator.Send(query);

            var gettraining = new GetByIdTrainingQuery(DialyActivity.TrainingId);
            Training = await _mediator.Send(gettraining);
            //check
            var checkeval = new CheckDialyUserEvaluationCommand(DialyActivity.Id, userId);
            bool check = await _mediator.Send(checkeval);
            if (check)
            {
                TempData["error"] = "Evaluation Already Taken";
                return RedirectToPage("EvaluationResult", new { did = DialyActivity.Id, userId = userId, tid = Training.Id });
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            StringBuilder formInfo = new StringBuilder();
            var eveluationData = new List<(long questionId, string answer, string userId, long dailyId)>();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //check
            var checkeval = new CheckDialyUserEvaluationCommand(DialyId, userId);
            bool check = await _mediator.Send(checkeval);
            if(check) {
                TempData["error"] = "Evaluation Already Taken";
                return RedirectToPage("EvaluationResult", new {did = DialyId, userId = userId, tid = TrainingId});
            }
            //long did, string userId, long tid

            foreach (var key in Request.Form.Keys)
            {
                string value = Request.Form[key];
                formInfo.AppendLine($"{key}: {value}");

                if (key.StartsWith("answer_"))
                {
                    if (long.TryParse(key.Substring("answer_".Length), out long questionId) && !string.IsNullOrWhiteSpace(value))
                    {
                        eveluationData.Add((questionId, value, userId, DialyId));
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
            var command = new UpdateDialyUserEvaluationListCommand(eveluationData);
            await _mediator.Send(command);

            TempData["success"] = "Evaluation submitted successfully.";
            //return RedirectToPage("./Success", new { exp = 3, id = TrainingId });
            return RedirectToPage("EvaluationResult", new { did = DialyId, userId = userId, tid = TrainingId });
        }

    }
}
