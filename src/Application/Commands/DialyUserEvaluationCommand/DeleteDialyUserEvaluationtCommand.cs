using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.DialyUserEvaluationCommand
{
      public sealed class DeleteDialyUserEvaluationCommand : IRequest
    {
        public DeleteDialyUserEvaluationCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteDialyUserEvaluationCommandHandler : IRequestHandler<DeleteDialyUserEvaluationCommand>
    {
        private readonly IDialyUserEvaluationRepository _repository;

        public DeleteDialyUserEvaluationCommandHandler(IDialyUserEvaluationRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteDialyUserEvaluationCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
