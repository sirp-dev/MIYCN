using Application.Validators;
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
    public sealed class GetByIdUserTestQuery : IRequest<UserTest>
    {
        public GetByIdUserTestQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdUserTestQueryHandler : IRequestHandler<GetByIdUserTestQuery, UserTest>
        {

            private readonly IUserTestRepository _repository;

            public GetByIdUserTestQueryHandler(IUserTestRepository repository)
            {
                _repository = repository;
            }
            public async Task<UserTest> Handle(GetByIdUserTestQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                UserTest data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
