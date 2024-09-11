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
    public interface ITrainingFacilitatorRepository : IRepository<TrainingFacilitator>
    { 
        Task<FacilitatorInTrainingDTo> FacilitatorInTraining(long trainingId, string userId);
        Task<List<FacilitatorInTrainingDTo>> FacilitatorInTraining(long trainingId);
        Task<List<FacilitatorInTrainingDTo>> AllFacilitator();

        Task<bool> AddFacilitator(TrainingFacilitator model);
        Task<TrainingFacilitator> GetFacilitatorById(long id);

        Task UpdateFacilitatorInTrainingStatus(long trainingId, long facilitatorId, FacilitatorTrainingStatus status, string reason);

    }
}
