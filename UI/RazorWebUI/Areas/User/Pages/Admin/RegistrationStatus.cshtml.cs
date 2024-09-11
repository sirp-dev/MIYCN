using Application.Commands.EmailCommand;
using Application.Commands.IdentityCommand;
using Application.Commands.SmsCommand;
using Application.Queries.IdentityQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PostmarkEmailService;
using System.Text.Encodings.Web;

namespace RazorWebUI.Areas.User.Pages.Admin
{
    public class RegistrationStatusModel : PageModel
    {

        private readonly ILogger<RegistrationStatusModel> _logger;
        private readonly IMediator _mediator;

        public RegistrationStatusModel(ILogger<RegistrationStatusModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [BindProperty]
        public BasicProfileDto UserDatas { get; private set; }
        [BindProperty]
        public bool SendEmail { get; set; }
        [BindProperty]
        public bool SendSMS { get; set; }
        [BindProperty]
        public string UserId { get; set; }
        [BindProperty]
        public string Rx { get;set; }
        [BindProperty]
        public long TrId { get;set; }
        public async Task<ActionResult> OnGetAsync(string id, string? rx, long txid = 0)
        {
            if (id == null)
            {
                return NotFound();
            }
            var query = new GetUserByIdQuery(id);
            UserDatas = await _mediator.Send(query);
            Rx = rx;
            TrId = txid;
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var query = new ResendSmsEmailCommand(UserId, SendSMS, SendEmail);
            var result = await _mediator.Send(query);
            
            if(result == true)
            {
                TempData["success"] = "Successful";
            }
            else
            {
                TempData["error"] = "Failed";

            }
            return RedirectToPage("./RegistrationStatus", new { id = UserId, rx = Rx, txid = TrId });
        }

    }

}