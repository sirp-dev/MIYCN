using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CertificateCommand
{
    public sealed class AddCertificateCommand : IRequest
    {
        public AddCertificateCommand(Certificate certificate)
        {
            Certificate = certificate;
        }

        public Certificate Certificate { get; set; }


    }

    public class AddCertificateCommandHandler : IRequestHandler<AddCertificateCommand>
    {
        private readonly ICertificateRepository _certificateRepository;

        public AddCertificateCommandHandler(ICertificateRepository certificateRepository)
        {
            _certificateRepository = certificateRepository;
        }

        public async Task Handle(AddCertificateCommand request, CancellationToken cancellationToken)
        {

            await _certificateRepository.AddAsync(request.Certificate);


        }
    }
}
