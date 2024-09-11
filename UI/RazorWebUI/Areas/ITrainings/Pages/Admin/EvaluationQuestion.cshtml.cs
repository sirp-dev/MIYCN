using Application.Commands.DialyEvaluationQuestionCommand;
using Application.Commands.TrainingCommand;
using Application.Queries.CertificationQueries;
using Application.Queries.DialyActivityQueries;
using Application.Queries.DialyUserEvaluationQueries;
using Application.Queries.EvaluationQuestionQueries;
using Application.Queries.ModuleTopicQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class EvaluationQuestionModel : PageModel
    {
        private readonly IMediator _mediator;

        public EvaluationQuestionModel(IMediator mediator)
        {
            _mediator = mediator;
        }



        public List<DialyEvaluationQuestion> ListDialyEvaluationQuestion { get; set; }
        public Training Training { get; private set; }
        public DialyActivity DialyActivity { get; set; }

        [BindProperty]
        public DialyEvaluationQuestion DialyEvaluationQuestion { get; set; }
        public List<SelectListItem> EvaluationQuestionDropdownDto { get; set; }
        public List<SelectListItem> ModuleTopicDropdownDto { get; set; }


        [BindProperty]
        public string? GeneralQuestion { get; set; }
        [BindProperty]
        public string? ModuleTopicQuestion { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }


            var query = new GetByIdDialyActivityQuery(id);
            DialyActivity = await _mediator.Send(query);

            GetByIdTrainingQuery TCommand = new GetByIdTrainingQuery(DialyActivity.TrainingId);
            Training = await _mediator.Send(TCommand);



            ListDialyEvaluationQuestionQuery ListCommand = new ListDialyEvaluationQuestionQuery(DialyActivity.Id);
            ListDialyEvaluationQuestion = await _mediator.Send(ListCommand);
            //
            var questiondropdown = new ListEvaluationQuestionQuery();
            var Listquestiondropdown = await _mediator.Send(questiondropdown);

            EvaluationQuestionDropdownDto = Listquestiondropdown
            .Select(eq => new SelectListItem
            {
                Value = eq.Question.ToString(),
                Text = eq.Question
            }).ToList();
            //
            var moduletopics = new ListModuleTopicQuery();
            var listmoduletopics = await _mediator.Send(moduletopics);

            listmoduletopics.Where(x=>x.Module != null).ToList();
            listmoduletopics.Where(x =>x.Module.Title != null).ToList();




            ModuleTopicDropdownDto = listmoduletopics
                .Where(eq => eq != null && eq.Title != null && eq.Module != null && eq.Module.Title != null)

            .Select(eq => new SelectListItem
            {
                Value = $"{eq.Title} ({eq.Module.Title})",
                Text = $"{eq.Title} ({eq.Module.Title})"
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (ModuleTopicQuestion != null)
            {
                DialyEvaluationQuestion.Question = ModuleTopicQuestion;
                DialyEvaluationQuestion.IsModuleTopic = true;
            }
            else if (GeneralQuestion != null)
            {
                DialyEvaluationQuestion.Question = GeneralQuestion;
            }

            try
            {
                AddDialyEvaluationQuestionCommand Command = new AddDialyEvaluationQuestionCommand(DialyEvaluationQuestion);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./EvaluationQuestion", new { id = DialyEvaluationQuestion.DialyActivityId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {


            try
            {
                DeleteDialyEvaluationQuestionCommand Command = new DeleteDialyEvaluationQuestionCommand(DialyEvaluationQuestion.Id);
                bool result = await _mediator.Send(Command);
                if (result == true)
                {
                    TempData["success"] = "Success";

                }
                else
                {
                    TempData["error"] = "Cannot be delete. Evalution already answered by participants";
                }
                return RedirectToPage("./EvaluationQuestion", new { id = DialyEvaluationQuestion.DialyActivityId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
