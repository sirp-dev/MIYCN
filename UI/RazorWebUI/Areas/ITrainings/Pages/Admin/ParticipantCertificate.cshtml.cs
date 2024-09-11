using Application.Commands.AttendanceCommand;
using Application.Queries.TrainingParticipantQueries;
using Application.Queries.TrainingQueries;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Migrations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Domain.Models.EnumStatus;
using System.Text;
using Application.Commands.CertificateCommand;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class ParticipantCertificateModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public ParticipantCertificateModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<ParticipantCertificateDto> Datas { get; private set; }
        public Training Training { get; private set; }

        [BindProperty]
        public long TrainingId { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            var query = new ListParticipantCertificateByTrainingIdQuery(id);
            Datas = await _mediator.Send(query);

            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            StringBuilder formInfo = new StringBuilder();
            var certificateData = new List<(long trainingId, CertificateType certificateType, string userId)>();
            // Initialize counters for each status
            int allCount = 0;

            foreach (var key in Request.Form.Keys)
            {
                string value = Request.Form[key];
                formInfo.AppendLine($"{key}: {value}");

                // Check if the key starts with "AttendanceResult_"
                if (key.StartsWith("CertificateStatus_"))
                {
                    string prefix = "CertificateStatus_";
                    string userId = key.Substring(prefix.Length);
                    // Add the extracted certificate ID and status to the list
                    certificateData.Add((TrainingId, CertificateType.Participant, userId));
                    // Increment the corresponding counter

                    allCount++;

                }
            }

            // Now you have certificateData populated with certificate IDs and statuses
            // Pass certificateData to the command handler
            var command = new AddCertificateListCommand(certificateData);
            await _mediator.Send(command);
            // Construct the TempData message with the counts
            string message = $"{allCount} were added for certificate";

            // Store the message in TempData
            TempData["response"] = message;
            return RedirectToPage("./TrainingCertificate", new
            {
                id = TrainingId, 
            });
            // Your existing code continues here...
        }

    }
}
