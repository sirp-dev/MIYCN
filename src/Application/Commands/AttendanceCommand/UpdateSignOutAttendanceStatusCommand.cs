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
    public sealed class UpdateSignOutAttendanceStatusCommand : IRequest
    {
        public UpdateSignOutAttendanceStatusCommand(List<(long attendanceId, AttendanceSignOutStatus status)> attendanceData)
        {
            AttendanceData = attendanceData;
        }

        public List<(long attendanceId, AttendanceSignOutStatus status)> AttendanceData { get; set; }
    }

    public class UpdateSignOutAttendanceStatusCommandHandler : IRequestHandler<UpdateSignOutAttendanceStatusCommand>
    {
        private readonly IAttendanceRepository _repository;

        public UpdateSignOutAttendanceStatusCommandHandler(IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateSignOutAttendanceStatusCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateSignOutAttendanceStatus(request.AttendanceData);
        }
    }

}
