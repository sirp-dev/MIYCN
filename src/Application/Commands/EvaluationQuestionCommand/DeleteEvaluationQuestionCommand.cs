using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EvaluationQuestionCommand
{
      public sealed class DeleteEvaluationQuestionCommand : IRequest
    {
        public DeleteEvaluationQuestionCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteEvaluationQuestionCommandHandler : IRequestHandler<DeleteEvaluationQuestionCommand>
    {
        private readonly IEvaluationQuestionRepository _repository;

        public DeleteEvaluationQuestionCommandHandler(IEvaluationQuestionRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteEvaluationQuestionCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
