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
    public sealed class ListDialyUserEvaluationQuery : IRequest<List<DialyUserEvaluation>>
    {

        public long Id { get; set; }

        public ListDialyUserEvaluationQuery(long id)
        {
            Id = id;
        }

        public class ListDialyUserEvaluationQueryHandler : IRequestHandler<ListDialyUserEvaluationQuery, List<DialyUserEvaluation>>
        {
            private readonly IDialyUserEvaluationRepository _repository;

            public ListDialyUserEvaluationQueryHandler(IDialyUserEvaluationRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<DialyUserEvaluation>> Handle(ListDialyUserEvaluationQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAll(request.Id);

            }
        }
    }

}
