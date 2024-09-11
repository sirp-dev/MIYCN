using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Application.Commands.CertificateCommand
{
    public sealed class UpdateCertificateCommand : IRequest
    {
        public UpdateCertificateCommand(Certificate certificate)
        {
            Certificate = certificate;
        }

        public Certificate Certificate { get; set; }


    }

    public class UpdateCertificateCommandHandler : IRequestHandler<UpdateCertificateCommand>
    {
        private readonly ICertificateRepository _repository;

        public UpdateCertificateCommandHandler(ICertificateRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateCertificateCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.Certificate);
        }
    }
}
