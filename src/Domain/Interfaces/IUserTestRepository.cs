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
    public interface IUserTestRepository : IRepository<UserTest>
    {
        Task<bool> CheckIfUserTookTest(string userid, long trainingId, int testType);

        Task UserTestSubmit(List<(long questionId, int answer, string userId, long trainingId)> assessmentData);

        Task<UserTestResultDto> UserTestResult(long trainingId, string userId, int testType);

        Task<List<UserTestListDto>> ListUserTestByTrainingId(long trainingId);

    }
}
