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
    public sealed class ListSponsorByTrainingQuery : IRequest<List<Sponsor>>
    {
        public long TrainingId { get; set; }

        public ListSponsorByTrainingQuery(long trainingId)
        {
            TrainingId = trainingId;
        }

        public class ListSponsorByTrainingQueryHandler : IRequestHandler<ListSponsorByTrainingQuery, List<Sponsor>>
        {
            private readonly ISponsorRepository _repository;

            public ListSponsorByTrainingQueryHandler(ISponsorRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Sponsor>> Handle(ListSponsorByTrainingQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAll(request.TrainingId);

            }
        }
    }
}
