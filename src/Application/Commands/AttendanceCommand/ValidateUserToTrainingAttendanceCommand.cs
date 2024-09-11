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
    public sealed class ValidateUserToTrainingAttendanceCommand : IRequest
    {
        public ValidateUserToTrainingAttendanceCommand(long trainingId)
        {
            TrainingId = trainingId;
        }

        public long TrainingId { get; set; }


    }

    public class ValidateUserToTrainingAttendanceCommandHandler : IRequestHandler<ValidateUserToTrainingAttendanceCommand>
    {
        private readonly IAttendanceRepository _movieRepository;

        public ValidateUserToTrainingAttendanceCommandHandler(IAttendanceRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task Handle(ValidateUserToTrainingAttendanceCommand request, CancellationToken cancellationToken)
        {

            await _movieRepository.ValidateUserToTrainingAttendance(request.TrainingId);


        }
    }
}
