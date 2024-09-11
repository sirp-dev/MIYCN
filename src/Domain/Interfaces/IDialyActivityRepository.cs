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
    public interface IDialyActivityRepository : IRepository<DialyActivity>
    {
        Task<List<DialyActivity>> GetActivityByTrainingId(long trainingId);
        Task<DialyActivity> GetActivityByTrainingIdAndDate(long trainingId, DateTime date);

    }
}
