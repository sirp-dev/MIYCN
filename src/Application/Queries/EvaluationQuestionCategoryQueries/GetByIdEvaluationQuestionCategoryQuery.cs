using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.EvaluationQuestionCategoryQueries
{
        public sealed class GetByIdEvaluationQuestionCategoryQuery : IRequest<EvaluationQuestionCategory>
    {
        public GetByIdEvaluationQuestionCategoryQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdEvaluationQuestionCategoryQueryHandler : IRequestHandler<GetByIdEvaluationQuestionCategoryQuery, EvaluationQuestionCategory>
        {

            private readonly IEvaluationQuestionCategoryRepository _repository;

            public GetByIdEvaluationQuestionCategoryQueryHandler(IEvaluationQuestionCategoryRepository repository)
            {
                _repository = repository;
            }
            public async Task<EvaluationQuestionCategory> Handle(GetByIdEvaluationQuestionCategoryQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                EvaluationQuestionCategory data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
