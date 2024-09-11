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
    public interface ITrainingParticipantRepository : IRepository<TrainingParticipant>
    {
        Task<ParticipantInTrainingDTo> ParticipantInTraining(long trainingId, string userId);
        Task<bool> CheckParticipantInTraining(long trainingId, string userId);
        Task UpdateParticipantInTrainingStatus(long trainingId, long participantId, ParticipantTrainingStatus status, string reason);
        Task<List<ParticipantInTrainingDTo>> ParticipantInTraining(long trainingId);
 
        Task<List<ParticipantInTrainingDTo>> AllParticipants();
        Task<TrainingParticipant> GetParticipantById(long id);
        Task<bool> AddParticipant(TrainingParticipant model);
    }
}
