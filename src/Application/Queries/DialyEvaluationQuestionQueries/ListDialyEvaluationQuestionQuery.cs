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
    public sealed class ListDialyEvaluationQuestionQuery : IRequest<List<DialyEvaluationQuestion>>
    {

        public long DialyActivityId { get; set; }

        public ListDialyEvaluationQuestionQuery(long dailyActivityId)
        {
            DialyActivityId = dailyActivityId;
        }

        public class ListDialyEvaluationQuestionQueryHandler : IRequestHandler<ListDialyEvaluationQuestionQuery, List<DialyEvaluationQuestion>>
        {
            private readonly IDialyEvaluationQuestionRepository _repository;

            public ListDialyEvaluationQuestionQueryHandler(IDialyEvaluationQuestionRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<DialyEvaluationQuestion>> Handle(ListDialyEvaluationQuestionQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllByDialyActivity(request.DialyActivityId);

            }
        }
    }
}
