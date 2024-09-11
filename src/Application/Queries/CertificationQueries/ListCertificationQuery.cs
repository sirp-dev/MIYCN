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
    public sealed class ListCertificateQuery : IRequest<List<Certificate>>
    {
        public class ListCertificateQueryHandler : IRequestHandler<ListCertificateQuery, List<Certificate>>
        {
            private readonly ICertificateRepository _repository;

            public ListCertificateQueryHandler(ICertificateRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Certificate>> Handle(ListCertificateQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }
}
