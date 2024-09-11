using Application.Validators;
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
     public sealed class GetByIdProfileCategoryListQuery : IRequest<ProfileCategoryList>
    {
        public GetByIdProfileCategoryListQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdProfileCategoryListQueryHandler : IRequestHandler<GetByIdProfileCategoryListQuery, ProfileCategoryList>
        {

            private readonly IProfileCategoryListRepository _repository;

            public GetByIdProfileCategoryListQueryHandler(IProfileCategoryListRepository repository)
            {
                _repository = repository;
            }
            public async Task<ProfileCategoryList> Handle(GetByIdProfileCategoryListQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                ProfileCategoryList data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
