using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Application.Commands.AttendanceCommand
{
    public sealed class UpdateSignInAttendanceStatusCommand : IRequest
    {
        public UpdateSignInAttendanceStatusCommand(List<(long attendanceId, AttendanceSignInStatus status)> attendanceData)
        {
            AttendanceData = attendanceData;
        }

        public List<(long attendanceId, AttendanceSignInStatus status)> AttendanceData { get; set; }
    }

    public class UpdateSignInAttendanceStatusCommandHandler : IRequestHandler<UpdateSignInAttendanceStatusCommand>
    {
        private readonly IAttendanceRepository _repository;

        public UpdateSignInAttendanceStatusCommandHandler(IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateSignInAttendanceStatusCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateSignInAttendanceStatus(request.AttendanceData);
        }
    }

}
