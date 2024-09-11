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
    public sealed class AddEvaluationQuestionCommand : IRequest
    {
        public AddEvaluationQuestionCommand(EvaluationQuestion trainingTest)
        {
            EvaluationQuestion = trainingTest;
        }

        public EvaluationQuestion EvaluationQuestion { get; set; }


    }

    public class AddEvaluationQuestionCommandHandler : IRequestHandler<AddEvaluationQuestionCommand>
    {
        private readonly IEvaluationQuestionRepository _EvaluationQuestionRepository;

        public AddEvaluationQuestionCommandHandler(IEvaluationQuestionRepository EvaluationQuestionRepository)
        {
            _EvaluationQuestionRepository = EvaluationQuestionRepository;
        }

        public async Task Handle(AddEvaluationQuestionCommand request, CancellationToken cancellationToken)
        {

            await _EvaluationQuestionRepository.AddAsync(request.EvaluationQuestion);


        }
    }
}
