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
    public sealed class GetCertificateByNumberQuery : IRequest<Certificate>
    {
        public GetCertificateByNumberQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }

        // Handler
        public class GetCertificateByNumberQueryHandler : IRequestHandler<GetCertificateByNumberQuery, Certificate>
        {

            private readonly ICertificateRepository _repository;

            public GetCertificateByNumberQueryHandler(ICertificateRepository repository)
            {
                _repository = repository;
            }
            public async Task<Certificate> Handle(GetCertificateByNumberQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                Certificate data = await _repository.GetCertificateByNumber(request.Id);

                return data;
            }
        }
    }
}
