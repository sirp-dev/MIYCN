using Domain.DTOs;
using Domain.GenericInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITrainingRepository : IRepository<Training>
    {
        Task<TrainingDto> GetTrainingByIdAndCounts(long id);
        Task<TrainingDto> GetTrainingByIdReport(long id);
        Task<List<TrainingByUserDto>> GetAllTrainingsByUserId(string userId);
        Task<List<Training>> GetAll(string? state);
        Task<IQueryable<Training>> GetAllTrainingWithDetails();
        Task RemoveTraining(long id);
        Task<List<Training>> GetAllByCategoryId(string? state, long id = 0);
        Task<List<TrainingDto>> GetTrainingByReportList();
    }
}
