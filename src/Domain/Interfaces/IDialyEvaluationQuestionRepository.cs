using Domain.GenericInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDialyEvaluationQuestionRepository : IRepository<DialyEvaluationQuestion>
    {
        Task<List<DialyEvaluationQuestion>> GetAllByDialyActivity(long dailyactivityId);
    }
}
