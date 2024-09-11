using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.AttendanceCommand
{
    public sealed class UpdateAttendanceCommand : IRequest
    {
        public UpdateAttendanceCommand(Attendance movie)
        {
            Attendance = movie;
        }

        public Attendance Attendance { get; set; }


    }

    public class UpdateAttendanceCommandHandler : IRequestHandler<UpdateAttendanceCommand>
    {
        private readonly IAttendanceRepository _repository;

        public UpdateAttendanceCommandHandler(IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.Attendance);
        }
    }
}
