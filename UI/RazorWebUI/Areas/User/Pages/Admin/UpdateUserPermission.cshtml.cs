using Amazon;
using Application.Commands.IdentityCommand;
using Application.Queries.IdentityQueries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.User.Pages.Admin
{
     
    public class UpdateUserPermissionModel : PageModel
    {


        private readonly ILogger<InfoModel> _logger;
        private readonly IMediator _mediator;

        public UpdateUserPermissionModel(ILogger<InfoModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public UserRolesDto UserDatas { get; private set; }

        [BindProperty]
        public string UserId { get; set; }

        [BindProperty]
        public string State { get; set; }

        public async Task<ActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var query = new GetUserPermissionListQuery(id);
            UserDatas = await _mediator.Send(query);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id, string userId, string fullname)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(id))
            {
                return BadRequest("User ID and Role ID must be provided");
            } 
            var command = new UpdateUserRoleCommand(userId, id, fullname);
            string userid = await _mediator.Send(command);
            return RedirectToPage("./UpdateUserPermission", new { id = userid });
        }

        public async Task<IActionResult> OnPostUpdateStateAsync()
        {
            if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(State))
            {
                TempData["error"] ="State must be provided";
                var query = new GetUserPermissionListQuery(UserId);
                UserDatas = await _mediator.Send(query);
            }
            var command = new UpdateUserStateCommand(State, UserId);
            var result = await _mediator.Send(command);
            if(result.Success) {
                TempData["success"]  = "Successful";
            }
            else
            {
                TempData["error"] = "Failed";
            }
            return RedirectToPage("./UpdateUserPermission", new { id = UserId });
        }

    }
}
