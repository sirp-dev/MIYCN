using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Application.Commands.DialyEvaluationQuestionCommand
{
    public sealed class UpdateDialyEvaluationQuestionCommand : IRequest
    {
        public UpdateDialyEvaluationQuestionCommand(DialyEvaluationQuestion certificate)
        {
            DialyEvaluationQuestion = certificate;
        }

        public DialyEvaluationQuestion DialyEvaluationQuestion { get; set; }


    }

    public class UpdateDialyEvaluationQuestionCommandHandler : IRequestHandler<UpdateDialyEvaluationQuestionCommand>
    {
        private readonly IDialyEvaluationQuestionRepository _repository;

        public UpdateDialyEvaluationQuestionCommandHandler(IDialyEvaluationQuestionRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateDialyEvaluationQuestionCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.DialyEvaluationQuestion);
        }
    }
}
