using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.ProfileCategoryQueries
{
        public sealed class GetByUserIdProfileCategoryQuery : IRequest<List<ProfileCategory>>
    {
        public GetByUserIdProfileCategoryQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }

        // Handler
        public class GetByUserIdProfileCategoryQueryHandler : IRequestHandler<GetByUserIdProfileCategoryQuery, List<ProfileCategory>>
        {

            private readonly IProfileCategoryRepository _repository;

            public GetByUserIdProfileCategoryQueryHandler(IProfileCategoryRepository repository)
            {
                _repository = repository;
            }
            public async Task<List<ProfileCategory>> Handle(GetByUserIdProfileCategoryQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                var data = await _repository.GetByUserId(request.Id);

                return data;
            }
        }
    }

}
