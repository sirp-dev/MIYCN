using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TimeTableQueries
{
        public sealed class ListTimeTableQuery : IRequest<List<TimeTable>>
    {

        public long TrainingId { get; set; }

        public ListTimeTableQuery(long trainingId)
        {
            TrainingId = trainingId;
        }

        public class ListTimeTableQueryHandler : IRequestHandler<ListTimeTableQuery, List<TimeTable>>
        {
            private readonly ITimeTableRepository _repository;

            public ListTimeTableQueryHandler(ITimeTableRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<TimeTable>> Handle(ListTimeTableQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAll(request.TrainingId);

            }
        }
    }

}
