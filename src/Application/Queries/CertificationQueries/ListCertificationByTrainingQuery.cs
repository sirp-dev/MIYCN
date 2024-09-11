using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.CertificationQueries
{
    public sealed class ListCertificationByTrainingQuery : IRequest<List<Certificate>>
    {

        public long TrainingId { get; set; }

        public ListCertificationByTrainingQuery(long trainingId)
        {
            TrainingId = trainingId;
        }

        public class ListCertificationByTrainingQueryHandler : IRequestHandler<ListCertificationByTrainingQuery, List<Certificate>>
        {
            private readonly ICertificateRepository _repository;

            public ListCertificationByTrainingQueryHandler(ICertificateRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Certificate>> Handle(ListCertificationByTrainingQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetCertificateByTrainingId(request.TrainingId);

            }
        }
    }
}
