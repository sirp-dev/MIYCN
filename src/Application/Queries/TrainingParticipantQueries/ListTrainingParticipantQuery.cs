using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TrainingParticipantQueries
{
    public sealed class ListTrainingParticipantQuery : IRequest<List<ParticipantInTrainingDTo>>
    {
        public class ListTrainingParticipantQueryHandler : IRequestHandler<ListTrainingParticipantQuery, List<ParticipantInTrainingDTo>>
        {
            private readonly ITrainingParticipantRepository _repository;

            public ListTrainingParticipantQueryHandler(ITrainingParticipantRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<ParticipantInTrainingDTo>> Handle(ListTrainingParticipantQuery request, CancellationToken cancellationToken)
            {
                return await _repository.AllParticipants();

            }
        }
    }

}
