using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Application.Commands.CertificateCommand
{
      public sealed class AddCertificateListCommand : IRequest
    {
        public AddCertificateListCommand(List<(long trainingId, CertificateType certificateType, string userId)> certificateData)
        {
            CertificateData = certificateData;
        }

        public List<(long trainingId, CertificateType certificateType, string userId)> CertificateData { get; set; }
    }

    public class AddCertificateListCommandHandler : IRequestHandler<AddCertificateListCommand>
    {
        private readonly ICertificateRepository _certificateRepository;

        public AddCertificateListCommandHandler(ICertificateRepository certificateRepository)
        {
            _certificateRepository = certificateRepository;
        }


        public async Task Handle(AddCertificateListCommand request, CancellationToken cancellationToken)
        {
            await _certificateRepository.AddParticipantForCertificate(request.CertificateData);
        }
    }

}
