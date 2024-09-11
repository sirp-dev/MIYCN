using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TimeTableCommand
{
     public sealed class AddTimeTableCommand : IRequest
    {
        public AddTimeTableCommand(TimeTable education)
        {
            TimeTable = education;
        }

        public TimeTable TimeTable { get; set; }


    }

    public class AddTimeTableCommandHandler : IRequestHandler<AddTimeTableCommand>
    {
        private readonly ITimeTableRepository _educationRepository;

        public AddTimeTableCommandHandler(ITimeTableRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        public async Task Handle(AddTimeTableCommand request, CancellationToken cancellationToken)
        {

            await _educationRepository.AddAsync(request.TimeTable);


        }
    }
}
