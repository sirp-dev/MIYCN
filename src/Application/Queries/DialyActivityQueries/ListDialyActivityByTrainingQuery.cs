using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.DialyActivityQueries
{
    public sealed class ListDialyActivityByTrainingQuery : IRequest<List<DialyActivity>>
    {

        public long TrainingId { get; set; }

        public ListDialyActivityByTrainingQuery(long trainingId)
        {
            TrainingId = trainingId;
        }

        public class ListDialyActivityByTrainingQueryHandler : IRequestHandler<ListDialyActivityByTrainingQuery, List<DialyActivity>>
        {
            private readonly IDialyActivityRepository _repository;

            public ListDialyActivityByTrainingQueryHandler(IDialyActivityRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<DialyActivity>> Handle(ListDialyActivityByTrainingQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetActivityByTrainingId(request.TrainingId);

            }
        }
    }

}
