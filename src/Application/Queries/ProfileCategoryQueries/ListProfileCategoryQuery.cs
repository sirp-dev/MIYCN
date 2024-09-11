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
     public sealed class ListProfileCategoryQuery : IRequest<List<ProfileCategory>>
    {
        public class ListProfileCategoryQueryHandler : IRequestHandler<ListProfileCategoryQuery, List<ProfileCategory>>
        {
            private readonly IProfileCategoryRepository _repository;

            public ListProfileCategoryQueryHandler(IProfileCategoryRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<ProfileCategory>> Handle(ListProfileCategoryQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }
}
