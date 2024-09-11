using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.UserTestQueries
{
    public sealed class ListUserTestQuery : IRequest<List<UserTest>>
    {
        public class ListUserTestQueryHandler : IRequestHandler<ListUserTestQuery, List<UserTest>>
        {
            private readonly IUserTestRepository _repository;

            public ListUserTestQueryHandler(IUserTestRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<UserTest>> Handle(ListUserTestQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }

}
