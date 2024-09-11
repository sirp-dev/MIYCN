using Application.Validators;
using Domain.DTOs;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.DialyUserEvaluationQueries
{
    public sealed class DialyUserEvaluationResultQuery : IRequest<DialyUserEvaluationResultDto>
    {
        public DialyUserEvaluationResultQuery(long dailyId, string userId)
        {
            DialyId = dailyId;
            UserId = userId; 
        }

        public long DialyId { get; }
        public string UserId { get; } 

        // Handler
        public class DialyUserEvaluationResultQueryHandler : IRequestHandler<DialyUserEvaluationResultQuery, DialyUserEvaluationResultDto>
        {

            private readonly IDialyUserEvaluationRepository _repository;

            public DialyUserEvaluationResultQueryHandler(IDialyUserEvaluationRepository repository)
            {
                _repository = repository;
            }
            public async Task<DialyUserEvaluationResultDto> Handle(DialyUserEvaluationResultQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                DialyUserEvaluationResultDto data = await _repository.DialyUserEvaluationResult(request.DialyId, request.UserId);

                return data;
            }
        }
    }
}
