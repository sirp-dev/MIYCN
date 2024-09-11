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
    public sealed class ListAttendanceQuery : IRequest<List<Attendance>>
    {
        public class ListAttendanceQueryHandler : IRequestHandler<ListAttendanceQuery, List<Attendance>>
        {
            private readonly IAttendanceRepository _repository;

            public ListAttendanceQueryHandler(IAttendanceRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Attendance>> Handle(ListAttendanceQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }

}
