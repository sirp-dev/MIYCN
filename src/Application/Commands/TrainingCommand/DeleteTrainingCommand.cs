using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingCommand
{
      public sealed class DeleteTrainingCommand : IRequest
    {
        public DeleteTrainingCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteTrainingCommandHandler : IRequestHandler<DeleteTrainingCommand>
    {
        private readonly ITrainingRepository _repository;

        public DeleteTrainingCommandHandler(ITrainingRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteTrainingCommand request, CancellationToken cancellationToken)
        {

            var data = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveTraining(request.Id);



        }
    }
}
