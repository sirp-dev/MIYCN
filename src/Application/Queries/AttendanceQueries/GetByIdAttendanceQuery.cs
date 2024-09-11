using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Validators;

namespace Application.Queries.AttendanceQueries
{
    public sealed class GetByIdAttendanceQuery : IRequest<Attendance>
    {
        public GetByIdAttendanceQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdAttendanceQueryHandler : IRequestHandler<GetByIdAttendanceQuery, Attendance>
        {

            private readonly IAttendanceRepository _repository;

            public GetByIdAttendanceQueryHandler(IAttendanceRepository repository)
            {
                _repository = repository;
            }
            public async Task<Attendance> Handle(GetByIdAttendanceQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                Attendance data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
