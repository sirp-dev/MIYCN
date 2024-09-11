using Application.Queries.DialyUserEvaluationQueries;
using Application.Queries.IdentityQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RazorWebUI.Pages.Shared.ViewComponents
{
     public class CheckEvaluationByDialyActivityIdByUserIdViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public CheckEvaluationByDialyActivityIdByUserIdViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<IViewComponentResult> InvokeAsync(long id, string userId)
        {
            var checkeval = new CheckDialyUserEvaluationCommand(id, userId);
            bool check = await _mediator.Send(checkeval);
            if (check)
            {
                TempData["check"] = "Evaluation Already Taken";
                 
            }
            TempData["id"]  = id;
            return View();
        }
    }
}
