using Application.Validators;
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
    public sealed class GetByIdTrainingParticipantQuery : IRequest<TrainingParticipant>
    {
        public GetByIdTrainingParticipantQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdTrainingParticipantQueryHandler : IRequestHandler<GetByIdTrainingParticipantQuery, TrainingParticipant>
        {

            private readonly ITrainingParticipantRepository _repository;

            public GetByIdTrainingParticipantQueryHandler(ITrainingParticipantRepository repository)
            {
                _repository = repository;
            }
            public async Task<TrainingParticipant> Handle(GetByIdTrainingParticipantQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                TrainingParticipant data = await _repository.GetParticipantById(request.Id);

                return data;
            }
        }
    }

}
