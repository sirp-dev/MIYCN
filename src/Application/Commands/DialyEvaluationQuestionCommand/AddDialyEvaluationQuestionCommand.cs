using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.DialyEvaluationQuestionCommand
{
    public sealed class AddDialyEvaluationQuestionCommand : IRequest
    {
        public AddDialyEvaluationQuestionCommand(DialyEvaluationQuestion certificate)
        {
            DialyEvaluationQuestion = certificate;
        }

        public DialyEvaluationQuestion DialyEvaluationQuestion { get; set; }


    }

    public class AddDialyEvaluationQuestionCommandHandler : IRequestHandler<AddDialyEvaluationQuestionCommand>
    {
        private readonly IDialyEvaluationQuestionRepository _certificateRepository;

        public AddDialyEvaluationQuestionCommandHandler(IDialyEvaluationQuestionRepository certificateRepository)
        {
            _certificateRepository = certificateRepository;
        }

        public async Task Handle(AddDialyEvaluationQuestionCommand request, CancellationToken cancellationToken)
        {

            await _certificateRepository.AddAsync(request.DialyEvaluationQuestion);


        }
    }
}
