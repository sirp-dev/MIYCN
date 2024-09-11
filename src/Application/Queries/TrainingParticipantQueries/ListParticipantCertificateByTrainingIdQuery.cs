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
    public sealed class ListParticipantCertificateByTrainingIdQuery : IRequest<List<ParticipantCertificateDto>>
    {

        public ListParticipantCertificateByTrainingIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        public class ListParticipantCertificateByTrainingIdQueryHandler : IRequestHandler<ListParticipantCertificateByTrainingIdQuery, List<ParticipantCertificateDto>>
        {
            private readonly ICertificateRepository _repository;

            public ListParticipantCertificateByTrainingIdQueryHandler(ICertificateRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<ParticipantCertificateDto>> Handle(ListParticipantCertificateByTrainingIdQuery request, CancellationToken cancellationToken)
            {
                return await _repository.ParticipantCertificateList(request.Id);

            }
        }
    }

}
