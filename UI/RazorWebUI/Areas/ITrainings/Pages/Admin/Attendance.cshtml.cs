using Application.Commands.AttendanceCommand;
using Application.Queries.AttendanceQueries;
using Application.Queries.DialyActivityQueries;
using Application.Queries.TrainingQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;
using static Domain.Models.EnumStatus;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class AttendanceModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public AttendanceModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IEnumerable<Attendance> Datas { get; private set; }
        public Training Training { get; private set; }

        public DialyActivity DialyActivity { get; set; }

        public AttendanceSignInStatus AttendanceStatus { get; set; }

        [BindProperty]
        public long TrainingId { get; set; }

        [BindProperty]
        public long DialyActivityId { get; set; }
        public async Task<IActionResult> OnGetAsync(long id, long aid)
        {
            if (id < 0)
            {
                return NotFound();
            }




            GetByIdDialyActivityQuery ACommand = new GetByIdDialyActivityQuery(aid);
            DialyActivity = await _mediator.Send(ACommand);

            GetByIdTrainingQuery Command = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(Command);
            if (Training.TrainingStatus != TrainingStatus.Completed)
            {
                ValidateUserToTrainingAttendanceCommand queryAttendance = new ValidateUserToTrainingAttendanceCommand(id);
                await _mediator.Send(queryAttendance);
            }
            var query = new ListAttendanceByActivityIdQuery(aid);
            Datas = await _mediator.Send(query);

            return Page();
        }



        public async Task<IActionResult> OnPostSignInAsync()
        {
            StringBuilder formInfo = new StringBuilder();
            var attendanceData = new List<(long attendanceId, AttendanceSignInStatus status)>();
            // Initialize counters for each status
            int presentCount = 0;
            int absentCount = 0;

            foreach (var key in Request.Form.Keys)
            {
                string value = Request.Form[key];
                formInfo.AppendLine($"{key}: {value}");

                // Check if the key starts with "AttendanceResult_"
                if (key.StartsWith("AttendanceSignInResult_"))
                {
                    // Extract the attendance ID from the key
                    if (long.TryParse(key.Substring("AttendanceSignInResult_".Length), out long attendanceId))
                    {
                        // Get the enum value from the form
                        if (Enum.TryParse(value, out Domain.Models.EnumStatus.AttendanceSignInStatus status))
                        {
                            // Add the extracted attendance ID and status to the list
                            attendanceData.Add((attendanceId, status));
                            // Increment the corresponding counter
                            switch (status)
                            {
                                case AttendanceSignInStatus.Present:
                                    presentCount++;
                                    break;
                                case AttendanceSignInStatus.Absent:
                                    absentCount++;
                                    break;

                            }
                        }
                        else
                        {
                            // Handle invalid enum value
                            // Perhaps return an error response or log the issue
                        }
                    }
                    else
                    {
                        // Handle invalid attendance ID
                        // Perhaps return an error response or log the issue
                    }
                }
            }

            // Now you have attendanceData populated with attendance IDs and statuses
            // Pass attendanceData to the command handler
            var command = new UpdateSignInAttendanceStatusCommand(attendanceData);
            await _mediator.Send(command);
            // Construct the TempData message with the counts
            string message = $"{presentCount} signin, {absentCount} not available";

            // Store the message in TempData
            TempData["response"] = message;
            return RedirectToPage("./Attendance", new { id = TrainingId, aid = DialyActivityId });
            // Your existing code continues here...
        }


        public async Task<IActionResult> OnPostSignOutAsync()
        {
            StringBuilder formInfo = new StringBuilder();
            var attendanceData = new List<(long attendanceId, AttendanceSignOutStatus status)>();
            // Initialize counters for each status
            int presentCount = 0;
            int absentCount = 0;

            foreach (var key in Request.Form.Keys)
            {
                string value = Request.Form[key];
                formInfo.AppendLine($"{key}: {value}");

                // Check if the key starts with "AttendanceResult_"
                if (key.StartsWith("AttendanceSignOutResult_"))
                {
                    // Extract the attendance ID from the key
                    if (long.TryParse(key.Substring("AttendanceSignOutResult_".Length), out long attendanceId))
                    {
                        // Get the enum value from the form
                        if (Enum.TryParse(value, out Domain.Models.EnumStatus.AttendanceSignOutStatus status))
                        {
                            // Add the extracted attendance ID and status to the list
                            attendanceData.Add((attendanceId, status));
                            // Increment the corresponding counter
                            switch (status)
                            {
                                case AttendanceSignOutStatus.Present:
                                    presentCount++;
                                    break;
                                case AttendanceSignOutStatus.Absent:
                                    absentCount++;
                                    break;

                            }
                        }
                        else
                        {
                            // Handle invalid enum value
                            // Perhaps return an error response or log the issue
                        }
                    }
                    else
                    {
                        // Handle invalid attendance ID
                        // Perhaps return an error response or log the issue
                    }
                }
            }

            // Now you have attendanceData populated with attendance IDs and statuses
            // Pass attendanceData to the command handler
            var command = new UpdateSignOutAttendanceStatusCommand(attendanceData);
            await _mediator.Send(command);
            // Construct the TempData message with the counts
            string message = $"{presentCount} signout, {absentCount} not available";

            // Store the message in TempData
            TempData["response"] = message;
            return RedirectToPage("./Attendance", new { id = TrainingId, aid = DialyActivityId });
            // Your existing code continues here...
        }


        public async Task<IActionResult> OnPostDeleteAttendanceAsync()
        {
            try
            {
                DeleteAttendanceCommand ACommand = new DeleteAttendanceCommand(DialyActivityId);
                await _mediator.Send(ACommand);
            }
            catch (Exception c) { }
            return RedirectToPage("./Attendance", new { id = TrainingId, aid = DialyActivityId });

        }
    }
}
