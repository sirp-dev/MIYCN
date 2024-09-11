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
    public sealed class ListSponsorQuery : IRequest<List<Sponsor>>
    {
        public class ListSponsorQueryHandler : IRequestHandler<ListSponsorQuery, List<Sponsor>>
        {
            private readonly ISponsorRepository _repository;

            public ListSponsorQueryHandler(ISponsorRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Sponsor>> Handle(ListSponsorQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }

}
