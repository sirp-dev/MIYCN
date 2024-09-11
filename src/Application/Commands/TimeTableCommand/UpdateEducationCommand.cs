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
      public sealed class UpdateTimeTableCommand : IRequest
    {
        public UpdateTimeTableCommand(TimeTable movie)
        {
            TimeTable = movie;
        }

        public TimeTable TimeTable { get; set; }


    }

    public class UpdateTimeTableCommandHandler : IRequestHandler<UpdateTimeTableCommand>
    {
        private readonly ITimeTableRepository _repository;

        public UpdateTimeTableCommandHandler(ITimeTableRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateTimeTableCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.TimeTable);
        }
    }
}
