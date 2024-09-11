using Application.Validators;
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
    public sealed class GetByIdCertificateQuery : IRequest<Certificate>
    {
        public GetByIdCertificateQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdCertificateQueryHandler : IRequestHandler<GetByIdCertificateQuery, Certificate>
        {

            private readonly ICertificateRepository _repository;

            public GetByIdCertificateQueryHandler(ICertificateRepository repository)
            {
                _repository = repository;
            }
            public async Task<Certificate> Handle(GetByIdCertificateQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                Certificate data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }
}
