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
    public sealed class ListEvaluationQuestionQuery : IRequest<List<EvaluationQuestion>>
    {
        public class ListEvaluationQuestionQueryHandler : IRequestHandler<ListEvaluationQuestionQuery, List<EvaluationQuestion>>
        {
            private readonly IEvaluationQuestionRepository _repository;

            public ListEvaluationQuestionQueryHandler(IEvaluationQuestionRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<EvaluationQuestion>> Handle(ListEvaluationQuestionQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllListAsync();

            }
        }
    }

}
