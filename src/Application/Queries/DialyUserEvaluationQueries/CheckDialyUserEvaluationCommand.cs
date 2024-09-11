using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.DialyUserEvaluationQueries
{
    public sealed class CheckDialyUserEvaluationCommand : IRequest<bool>
    {
        public CheckDialyUserEvaluationCommand(long dailyId, string userId)
        {
            DialyId = dailyId;
            UserId = userId; 
        }

        public long DialyId { get; }
        public string UserId { get; } 

        // Handler
        public class CheckDialyUserEvaluationCommandHandler : IRequestHandler<CheckDialyUserEvaluationCommand, bool>
        {

            private readonly IDialyUserEvaluationRepository _repository;

            public CheckDialyUserEvaluationCommandHandler(IDialyUserEvaluationRepository repository)
            {
                _repository = repository;
            }
            public async Task<bool> Handle(CheckDialyUserEvaluationCommand request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                bool data = await _repository.CheckIfUserTookEvaluation(request.UserId, request.DialyId);

                return data;
            }
        }
    }

}
