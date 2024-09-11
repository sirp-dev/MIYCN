using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Encodings.Web;
using MediatR;
using Application.Commands.IdentityCommand;
using Application.Queries.IdentityQueries;
using Application.Commands.EmailCommand;
using PostmarkEmailService;
using Application.Commands.SmsCommand;


namespace RazorWebUI.Areas.User.Pages.Admin
{
    public class AddModel : PageModel
    {
        private readonly IMediator _mediator;

        public AddModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public RegisterDto RegisterDto { get; set; }

        [BindProperty]
        public string Role { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                AddUserCommand regCommand = new AddUserCommand(RegisterDto, Role);
                var response = await _mediator.Send(regCommand);

                var userdt = new GetUserByIdQuery(response.UserId);
                var userresponse = await _mediator.Send(userdt);
                try
                {
                    string baseUrl = "https://miycnportal.ng/"; // replace with your application base URL
                    string relativeUrl = "/Identity/Account/Login";
                    Uri baseUri = new Uri(baseUrl);
                    Uri loginUri = new Uri(baseUri, relativeUrl);

                    // Now you can use the loginUri to create the link
                    string linkcomplete = loginUri.AbsoluteUri;

                    string datavalue = $"<a href='{HtmlEncoder.Default.Encode(linkcomplete)}'>click here to login</a>.";

                    string messagedetails = $"" + $"<h4>You are welcome to start your journey on the MIYCN Training. <br>Kindly use the following credentials to have access to the Portal and note to change your temporary password after login in.</h4>" +
                        
                        $"<br>Login Email: {userresponse.Email} <br>Temporal Password: {userresponse.TempPass}<br>" +
                        $"" + datavalue + "or copy the link before and paste in your browser to continue<br><br>" +
                        linkcomplete + "<br>";
                        
                    //send email
                    MessageDto msn = new MessageDto();
                    msn.Email = userresponse.Email;
                    msn.Message = messagedetails;
                    msn.Subject = "MIYCN Training";
                    msn.Name = userresponse.FullnameX ;
                    SendMessageCommand emailcommand = new SendMessageCommand(msn);
                    PostmarkResponse responseemail = await _mediator.Send(emailcommand);
                    if (responseemail.Status == PostmarkStatus.Success)
                    {
 

                    }

                }
                catch (Exception e)
                {

                }
                try
                {
                    SendSmsCommand smscommand = new SendSmsCommand(userresponse.PhoneNumber, "Dearest, \r\n\r\nKindly check your email to find your credentials for your MIYCN PORTAL. \r\n\r\nThanks.");
                    var smsresponse = await _mediator.Send(smscommand);
                     
                }
                catch (Exception c) { }
                return RedirectToPage("./RegistrationStatus", new { id = response.UserId });
            }
            catch (Exception ex)
            {
                return Page();

            }
        }

    }

}
