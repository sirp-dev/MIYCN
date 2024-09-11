using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.AttendanceQueries
{
    public sealed class ListAttendanceByActivityIdQuery : IRequest<List<Attendance>>
    {
        public long ActivityId { get; set; }

        public ListAttendanceByActivityIdQuery(long activityId)
        {
            ActivityId = activityId;
        }

        public class ListAttendanceByActivityIdQueryHandler : IRequestHandler<ListAttendanceByActivityIdQuery, List<Attendance>>
        {
            private readonly IAttendanceRepository _repository;

            public ListAttendanceByActivityIdQueryHandler(IAttendanceRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Attendance>> Handle(ListAttendanceByActivityIdQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAttendanceByActivity(request.ActivityId);

            }
        }
    }

}
