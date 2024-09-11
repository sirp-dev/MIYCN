using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Queries.EvaluationQuestionCategoryQueries
{
    public sealed class ListEvaluationQuestionCategoryQuery : IRequest<List<EvaluationQuestionCategory>>
    {
        public class ListEvaluationQuestionCategoryQueryHandler : IRequestHandler<ListEvaluationQuestionCategoryQuery, List<EvaluationQuestionCategory>>
        {
            private readonly IEvaluationQuestionCategoryRepository _repository;

            public ListEvaluationQuestionCategoryQueryHandler(IEvaluationQuestionCategoryRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<EvaluationQuestionCategory>> Handle(ListEvaluationQuestionCategoryQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }

}
