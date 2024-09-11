using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EvaluationQuestionCategoryCommand
{
      public sealed class DeleteEvaluationQuestionCategoryCommand : IRequest
    {
        public DeleteEvaluationQuestionCategoryCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteEvaluationQuestionCategoryCommandHandler : IRequestHandler<DeleteEvaluationQuestionCategoryCommand>
    {
        private readonly IEvaluationQuestionCategoryRepository _repository;

        public DeleteEvaluationQuestionCategoryCommandHandler(IEvaluationQuestionCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteEvaluationQuestionCategoryCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
