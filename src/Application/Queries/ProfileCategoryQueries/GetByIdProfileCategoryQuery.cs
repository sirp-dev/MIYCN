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
     public sealed class GetByIdProfileCategoryQuery : IRequest<ProfileCategory>
    {
        public GetByIdProfileCategoryQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdProfileCategoryQueryHandler : IRequestHandler<GetByIdProfileCategoryQuery, ProfileCategory>
        {

            private readonly IProfileCategoryRepository _repository;

            public GetByIdProfileCategoryQueryHandler(IProfileCategoryRepository repository)
            {
                _repository = repository;
            }
            public async Task<ProfileCategory> Handle(GetByIdProfileCategoryQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                ProfileCategory data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
