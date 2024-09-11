using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TimeTableCommand
{
      public sealed class DeleteTimeTableCommand : IRequest
    {
        public DeleteTimeTableCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteTimeTableCommandHandler : IRequestHandler<DeleteTimeTableCommand>
    {
        private readonly ITimeTableRepository _repository;

        public DeleteTimeTableCommandHandler(ITimeTableRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteTimeTableCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
