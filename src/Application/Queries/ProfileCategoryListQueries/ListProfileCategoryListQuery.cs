using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.ProfileCategoryListQueries
{
     public sealed class ListProfileCategoryListQuery : IRequest<List<ProfileCategoryList>>
    {
        public class ListProfileCategoryListQueryHandler : IRequestHandler<ListProfileCategoryListQuery, List<ProfileCategoryList>>
        {
            private readonly IProfileCategoryListRepository _repository;

            public ListProfileCategoryListQueryHandler(IProfileCategoryListRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<ProfileCategoryList>> Handle(ListProfileCategoryListQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }
}
