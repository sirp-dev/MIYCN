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
     public sealed class AddAttendanceCommand : IRequest
    {
        public AddAttendanceCommand(Attendance movie)
        {
            Attendance = movie;
        }

        public Attendance Attendance { get; set; }


    }

    public class AddAttendanceCommandHandler : IRequestHandler<AddAttendanceCommand>
    {
        private readonly IAttendanceRepository _movieRepository;

        public AddAttendanceCommandHandler(IAttendanceRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task Handle(AddAttendanceCommand request, CancellationToken cancellationToken)
        {

            await _movieRepository.AddAsync(request.Attendance);


        }
    }
}
