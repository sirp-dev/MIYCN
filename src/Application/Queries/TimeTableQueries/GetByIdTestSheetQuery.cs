using Application.Validators;
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
      public sealed class GetByIdTimeTableQuery : IRequest<TimeTable>
    {
        public GetByIdTimeTableQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdTimeTableQueryHandler : IRequestHandler<GetByIdTimeTableQuery, TimeTable>
        {

            private readonly ITimeTableRepository _repository;

            public GetByIdTimeTableQueryHandler(ITimeTableRepository repository)
            {
                _repository = repository;
            }
            public async Task<TimeTable> Handle(GetByIdTimeTableQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                TimeTable data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
