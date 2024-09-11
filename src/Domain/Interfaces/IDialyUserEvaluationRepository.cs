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
 
    public interface IDialyUserEvaluationRepository : IRepository<DialyUserEvaluation>
    {
        Task<bool> CheckIfUserTookEvaluation(string userid, long dailyId);
        Task<bool> CheckIfEvaluationHasBeenTaken(long eveluationQuestionId);

        Task DialyUserEvaluationSubmit(List<(long questionId, string answer, string userId, long dailyId)> evaluationData);

        Task<DialyUserEvaluationResultDto> DialyUserEvaluationResult(long dailyId, string userId);

        Task<List<DialyUserEvaluation>> GetAll(long id);
    }
}
