using Application.Queries.IdentityQueries;
using Application.Queries.TrainingCategoryQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class IndexModel : PageModel
    {
       

        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public TrainingCategory TrainingCategory { get; set; }
        public IEnumerable<Training> Datas { get; private set; }

        public async Task OnGetAsync(long id)
        {
            var getTrainingCategory = new GetByIdTrainingCategoryQuery(id);
            var result = await _mediator.Send(getTrainingCategory);
            if (result == null)
            {
                id = 0;
            }
            else
            {
                TrainingCategory = result;
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            string state = "";
            var usercommand = new GetUserByIdQuery(userId);
            var userinfo = await _mediator.Send(usercommand);
            if(userinfo != null)
            {
                state = userinfo.State;
            }
            if(User.IsInRole("Admin") || User.IsInRole("mSuperAdmin"))
            {
                state = "all";
            }
            var query = new ListTrainingByCategoryQuery(state, id);
            Datas = await _mediator.Send(query);
        }

    }
}
