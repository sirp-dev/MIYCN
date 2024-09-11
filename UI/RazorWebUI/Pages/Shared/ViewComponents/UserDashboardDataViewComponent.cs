using Application.Queries.IdentityQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RazorWebUI.Pages.Shared.ViewComponents
{
    public class UserDashboardDataViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public UserDashboardDataViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser = HttpContext.User;
            GetUserByNameQuery command = new GetUserByNameQuery(currentUser.Identity.Name);
            var basic = await _mediator.Send(command);
            return View(basic);
        }
    }
}
