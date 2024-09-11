using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.DialyUserEvaluationCommand
{
     public sealed class AddDialyUserEvaluationCommand : IRequest
    {
        public AddDialyUserEvaluationCommand(DialyUserEvaluation userTest)
        {
            DialyUserEvaluation = userTest;
        }

        public DialyUserEvaluation DialyUserEvaluation { get; set; }


    }

    public class AddDialyUserEvaluationCommandHandler : IRequestHandler<AddDialyUserEvaluationCommand>
    {
        private readonly IDialyUserEvaluationRepository _DialyUserEvaluationRepository;

        public AddDialyUserEvaluationCommandHandler(IDialyUserEvaluationRepository DialyUserEvaluationRepository)
        {
            _DialyUserEvaluationRepository = DialyUserEvaluationRepository;
        }

        public async Task Handle(AddDialyUserEvaluationCommand request, CancellationToken cancellationToken)
        {

            await _DialyUserEvaluationRepository.AddAsync(request.DialyUserEvaluation);


        }
    }
}
