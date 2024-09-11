using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.SponsorQueries
{
        public sealed class GetByIdSponsorQuery : IRequest<Sponsor>
    {
        public GetByIdSponsorQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdSponsorQueryHandler : IRequestHandler<GetByIdSponsorQuery, Sponsor>
        {

            private readonly ISponsorRepository _repository;

            public GetByIdSponsorQueryHandler(ISponsorRepository repository)
            {
                _repository = repository;
            }
            public async Task<Sponsor> Handle(GetByIdSponsorQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                Sponsor data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
