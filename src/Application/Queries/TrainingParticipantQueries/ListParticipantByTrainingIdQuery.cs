using Application.Queries.TrainingParticipantQueries;
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
    public sealed class ListParticipantByTrainingIdQuery : IRequest<List<ParticipantInTrainingDTo>>
    {

        public ListParticipantByTrainingIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        public class ListParticipantByTrainingIdQueryHandler : IRequestHandler<ListParticipantByTrainingIdQuery, List<ParticipantInTrainingDTo>>
        {
            private readonly ITrainingParticipantRepository _repository;

            public ListParticipantByTrainingIdQueryHandler(ITrainingParticipantRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<ParticipantInTrainingDTo>> Handle(ListParticipantByTrainingIdQuery request, CancellationToken cancellationToken)
            {
                return await _repository.ParticipantInTraining(request.Id);

            }
        }
    }

}
