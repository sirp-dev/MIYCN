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
    public sealed class GetByIdDialyUserEvaluationQuery : IRequest<DialyUserEvaluation>
    {
        public GetByIdDialyUserEvaluationQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdDialyUserEvaluationQueryHandler : IRequestHandler<GetByIdDialyUserEvaluationQuery, DialyUserEvaluation>
        {

            private readonly IDialyUserEvaluationRepository _repository;

            public GetByIdDialyUserEvaluationQueryHandler(IDialyUserEvaluationRepository repository)
            {
                _repository = repository;
            }
            public async Task<DialyUserEvaluation> Handle(GetByIdDialyUserEvaluationQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                DialyUserEvaluation data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
