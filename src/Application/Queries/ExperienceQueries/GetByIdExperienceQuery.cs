using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.ExperienceQueries
{
        public sealed class GetByIdExperienceQuery : IRequest<Experience>
    {
        public GetByIdExperienceQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdExperienceQueryHandler : IRequestHandler<GetByIdExperienceQuery, Experience>
        {

            private readonly IExperienceRepository _repository;

            public GetByIdExperienceQueryHandler(IExperienceRepository repository)
            {
                _repository = repository;
            }
            public async Task<Experience> Handle(GetByIdExperienceQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                Experience data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
