using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.EducationQueries
{
        public sealed class GetByIdEducationQuery : IRequest<Education>
    {
        public GetByIdEducationQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdEducationQueryHandler : IRequestHandler<GetByIdEducationQuery, Education>
        {

            private readonly IEducationRepository _repository;

            public GetByIdEducationQueryHandler(IEducationRepository repository)
            {
                _repository = repository;
            }
            public async Task<Education> Handle(GetByIdEducationQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                Education data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
