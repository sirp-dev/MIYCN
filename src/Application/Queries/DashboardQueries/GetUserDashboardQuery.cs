using Application.Validators;
using Domain.DTOs;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.DashboardQueries
{
    public sealed class GetUserDashboardQuery : IRequest<UserDashboardDto>
    {
        public GetUserDashboardQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }

        public class GetUserDashboardQueryHandler : IRequestHandler<GetUserDashboardQuery, UserDashboardDto>
        {

            private readonly IDashboardRepository _repository;

            public GetUserDashboardQueryHandler(IDashboardRepository repository)
            {
                _repository = repository;
            }
            public async Task<UserDashboardDto> Handle(GetUserDashboardQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                UserDashboardDto data = await _repository.UserDashboardData(request.UserId);

                return data;
            }
        }
    }
}
