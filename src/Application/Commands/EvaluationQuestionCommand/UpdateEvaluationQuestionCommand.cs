using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EvaluationQuestionCommand
{
     public sealed class UpdateEvaluationQuestionCommand : IRequest
    {
        public UpdateEvaluationQuestionCommand(EvaluationQuestion movie)
        {
            EvaluationQuestion = movie;
        }

        public EvaluationQuestion EvaluationQuestion { get; set; }


    }

    public class UpdateEvaluationQuestionCommandHandler : IRequestHandler<UpdateEvaluationQuestionCommand>
    {
        private readonly IEvaluationQuestionRepository _repository;

        public UpdateEvaluationQuestionCommandHandler(IEvaluationQuestionRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateEvaluationQuestionCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.EvaluationQuestion);
        }
    }
}
