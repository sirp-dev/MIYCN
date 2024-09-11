using Application.Queries.IdentityQueries;
using Application.Queries.TrainingQueries;
using Application.Queries.UserTestQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Security.Cryptography;

namespace RazorWebUI.Areas.ITrainings.Pages.Account
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class ResultModel : PageModel
    {
        private readonly IMediator _mediator;

        public ResultModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public UserTestResultDto UserTestResultDto { get; set; }
        public TrainingDto Training { get; set; }
        public string Title { get; set; }
        public string Instruction { get; set; }

        public BasicProfileDto BasicProfileDto { get; set; }
        public async Task<IActionResult> OnGetAsync(long tid, string userId, int xty)
        {
            if (tid < 0)
            {
                return NotFound();
            }
            if (userId == null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            else
            {
                GetUserByIdQuery userCommand = new GetUserByIdQuery(userId);
                BasicProfileDto = await _mediator.Send(userCommand);
            }
            TestResultQuery Command = new TestResultQuery(tid, userId, xty);
            UserTestResultDto = await _mediator.Send(Command);

            if (UserTestResultDto != null)
            {
                if (UserTestResultDto.UserTest.Count() == 0)
                {
                    TempData["error"] = "No Assessment Found";
                    if (User.IsInRole("Participant"))
                    {
                        return RedirectToPage("./info", new { id = tid });
                    }
                    return RedirectToPage("/Admin/Test", new { id = tid });
                }
            }

            GetByIdAndCountTrainingQuery TCommand = new GetByIdAndCountTrainingQuery(tid);
            Training = await _mediator.Send(TCommand);

            if (xty == 0)
            {
                Title = "PRE TEST";
                Instruction = Training.PreTestInstruction;

            }
            else if (xty == 2)
            {
                Title = "POST TEST";
                Instruction = Training.PostTestInstruction;

            }
            return Page();
        }
    }
}
