using Domain.DTOs;
using Domain.GenericInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.Interfaces
{
    public interface ICertificateRepository : IRepository<Certificate>
    {

        Task<List<Certificate>> GetCertificateByTrainingId(long trainingId);
        Task<List<ParticipantCertificateDto>> ParticipantCertificateList(long trainingId);
        Task AddParticipantForCertificate(List<(long trainingId, CertificateType certificateType, string userId)> certificateData);

        Task<Certificate> GetCertificateByNumber(string number);
    }
}
