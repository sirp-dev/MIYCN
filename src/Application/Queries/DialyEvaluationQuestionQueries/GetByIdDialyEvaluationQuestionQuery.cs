using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.CertificationQueries
{
    public sealed class GetByIdDialyEvaluationQuestionQuery : IRequest<DialyEvaluationQuestion>
    {
        public GetByIdDialyEvaluationQuestionQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdDialyEvaluationQuestionQueryHandler : IRequestHandler<GetByIdDialyEvaluationQuestionQuery, DialyEvaluationQuestion>
        {

            private readonly IDialyEvaluationQuestionRepository _repository;

            public GetByIdDialyEvaluationQuestionQueryHandler(IDialyEvaluationQuestionRepository repository)
            {
                _repository = repository;
            }
            public async Task<DialyEvaluationQuestion> Handle(GetByIdDialyEvaluationQuestionQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                DialyEvaluationQuestion data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }
}
