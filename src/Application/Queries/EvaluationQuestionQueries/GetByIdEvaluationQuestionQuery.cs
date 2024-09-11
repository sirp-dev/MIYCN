using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.EvaluationQuestionQueries
{
    public sealed class GetByIdEvaluationQuestionQuery : IRequest<EvaluationQuestion>
    {
        public GetByIdEvaluationQuestionQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdEvaluationQuestionQueryHandler : IRequestHandler<GetByIdEvaluationQuestionQuery, EvaluationQuestion>
        {

            private readonly IEvaluationQuestionRepository _repository;

            public GetByIdEvaluationQuestionQueryHandler(IEvaluationQuestionRepository repository)
            {
                _repository = repository;
            }
            public async Task<EvaluationQuestion> Handle(GetByIdEvaluationQuestionQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                EvaluationQuestion data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
