using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EvaluationQuestionCategoryCommand
{
      public sealed class UpdateEvaluationQuestionCategoryCommand : IRequest
    {
        public UpdateEvaluationQuestionCategoryCommand(EvaluationQuestionCategory movie)
        {
            EvaluationQuestionCategory = movie;
        }

        public EvaluationQuestionCategory EvaluationQuestionCategory { get; set; }


    }

    public class UpdateEvaluationQuestionCategoryCommandHandler : IRequestHandler<UpdateEvaluationQuestionCategoryCommand>
    {
        private readonly IEvaluationQuestionCategoryRepository _repository;

        public UpdateEvaluationQuestionCategoryCommandHandler(IEvaluationQuestionCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateEvaluationQuestionCategoryCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.EvaluationQuestionCategory);
        }
    }
}
