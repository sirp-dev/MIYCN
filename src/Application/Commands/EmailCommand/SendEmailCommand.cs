using MediatR;
using PostmarkEmailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EmailCommand
{
    public class SendMessageCommand : IRequest<PostmarkResponse>
    {
        public MessageDto Message { get; set; }

        public SendMessageCommand(MessageDto message)
        {
            Message = message;
        }
    }

    public class SendMessageHandler : IRequestHandler<SendMessageCommand, PostmarkResponse>
    {
        private readonly PostmarkClient _postmarkService;

        public SendMessageHandler(PostmarkClient postmarkService)
        {
            _postmarkService = postmarkService;
        }

        public async Task<PostmarkResponse> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {

            string emailTemplate = @"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>//CompanyName//</title>
</head>
<body style=""font-family: Arial, sans-serif; margin: 0; padding: 0;"">

    <table role=""presentation"" cellspacing=""0"" cellpadding=""0"" width=""100%"" style=""border-collapse: collapse;"">
        <tr>
            <td style=""padding: 20px 0; text-align: center;"">
                <img src=""//YOUR_LOGO_URL//"" alt=""Company Logo"" width=""200"" style=""display: inline-block;"">
            </td>
        </tr>
        <tr>
            <td style=""background-color: #f4f4f4; padding: 20px;"">
                <h2 style=""color: #333333;"">HELLO //Recipient_Name//,</h2>    
<h4 style=""color: #333333;"">//Subject//.</h4>


                <p style=""color: #666666;"">
                    //Body//
                </p>
            </td>
        </tr>
        <tr>
            <td style=""background-color: #333333; color: #ffffff; padding: 10px; text-align: center;"">
                <p>&copy; //Date// //CompanyName// | Contact us at: <a href=""mailto://CompanyEmail//"" style=""color: #ffffff; text-decoration: none;"">//CompanyEmail//</a></p>
            </td>
        </tr>
    </table>

</body>
</html>
";
            emailTemplate = emailTemplate.Replace("//CompanyName//", "MIYCN Training");
            emailTemplate = emailTemplate.Replace("//YOUR_LOGO_URL//", "https://miycnportal.ng/img/miycn-logo-x2.png");
            emailTemplate = emailTemplate.Replace("//Subject//", request.Message.Subject.Replace("\r\n", ""));
            emailTemplate = emailTemplate.Replace("//Body//", request.Message.Message);
            emailTemplate = emailTemplate.Replace("//CompanyEmail//", "admin@miycnportal.com");
            emailTemplate = emailTemplate.Replace("//Date//", DateTime.UtcNow.Year.ToString());
            emailTemplate = emailTemplate.Replace("//Recipient_Name//", request.Message.Name);


            PostmarkResponse response = new PostmarkResponse();
            var message = new PostmarkMessage
            {
                From = "training@miycnportal.ng",
                //From = "help@koboview.com",
                To = request.Message.Email,
                Subject = request.Message.Subject,
                HtmlBody = emailTemplate
            };
            return null;

            //return await _postmarkService.SendMessageAsync(message);
        }
    }
}
