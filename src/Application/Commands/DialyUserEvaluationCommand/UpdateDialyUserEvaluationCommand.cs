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
     public sealed class UpdateDialyUserEvaluationCommand : IRequest
    {
        public UpdateDialyUserEvaluationCommand(DialyUserEvaluation movie)
        {
            DialyUserEvaluation = movie;
        }

        public DialyUserEvaluation DialyUserEvaluation { get; set; }


    }

    public class UpdateDialyUserEvaluationCommandHandler : IRequestHandler<UpdateDialyUserEvaluationCommand>
    {
        private readonly IDialyUserEvaluationRepository _repository;

        public UpdateDialyUserEvaluationCommandHandler(IDialyUserEvaluationRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateDialyUserEvaluationCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.DialyUserEvaluation);
        }
    }
}
