using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingTestCommand
{
      public sealed class DeleteTrainingTestCommand : IRequest
    {
        public DeleteTrainingTestCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteTrainingTestCommandHandler : IRequestHandler<DeleteTrainingTestCommand>
    {
        private readonly ITrainingTestRepository _repository;

        public DeleteTrainingTestCommandHandler(ITrainingTestRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteTrainingTestCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
