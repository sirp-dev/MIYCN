using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Infrastructure.Repositories
{
    public class DialyUserEvaluationRepository : Repository<DialyUserEvaluation>, IDialyUserEvaluationRepository
    {
        private readonly AppDbContext _context;

        public DialyUserEvaluationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckIfEvaluationHasBeenTaken(long eveluationQuestionId)
        {
            var result = await _context.DialyUserEvaluations.FirstOrDefaultAsync(x => x.DialyEvaluationQuestionId == eveluationQuestionId);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CheckIfUserTookEvaluation(string userid, long dailyId)
        {
            var data = await _context.DialyUserEvaluations.Where(x => x.DialyActivityId == dailyId && x.UserId == userid && x.Submitted == true
            ).FirstOrDefaultAsync();
            if (data != null)
            {
                return true;
            }
            return false;
        }

        public async Task<DialyUserEvaluationResultDto> DialyUserEvaluationResult(long dailyId, string userId)
        {
            DialyUserEvaluationResultDto result = new DialyUserEvaluationResultDto();
            var userresult = await _context.DialyUserEvaluations
               .Include(x => x.DialyEvaluationQuestion)
               .Where(x => x.DialyActivityId == dailyId && x.UserId == userId).ToListAsync();

            result.DialyUserEvaluation = userresult;




            return result;
        }

        public async Task DialyUserEvaluationSubmit(List<(long questionId, string answer, string userId, long dailyId)> evaluationData)
        {
            foreach (var (questionId, answer, userId, dailyId) in evaluationData)
            {
                var existingEvaluation = await _context.DialyUserEvaluations
                    .FirstOrDefaultAsync(e => e.UserId == userId && e.DialyActivityId == dailyId && e.DialyEvaluationQuestionId == questionId);

                if (existingEvaluation == null)
                {
                    var newEvaluation = new DialyUserEvaluation
                    {
                        UserId = userId,
                        DialyActivityId = dailyId,
                        Answer = answer,
                        DialyEvaluationQuestionId = questionId,
                        Submitted = true,
                        Date = DateTime.UtcNow.AddHours(1)
                    };
                    await _context.DialyUserEvaluations.AddAsync(newEvaluation);
                }
                else
                {
                    // Optionally, you could update the existing record if needed
                    existingEvaluation.Answer = answer;
                    existingEvaluation.Submitted = true;
                    _context.DialyUserEvaluations.Update(existingEvaluation);
                }
            }

            await _context.SaveChangesAsync();
            //foreach (var (questionId, answer, userId, dailyId) in evaluationData)
            //{
            //    DialyUserEvaluation nxtest = new DialyUserEvaluation();
            //    nxtest.UserId = userId;
            //    nxtest.DialyActivityId = dailyId;
            //    nxtest.Answer = answer;
            //    nxtest.DialyEvaluationQuestionId = questionId;
            //    nxtest.Submitted = true;
            //    await _context.DialyUserEvaluations.AddAsync(nxtest);
            //}


            //await _context.SaveChangesAsync();
        }

        public async Task<List<DialyUserEvaluation>> GetAll(long id)
        {
            var list = await _context.DialyUserEvaluations.Include(x => x.User)
                .Include(x => x.DialyEvaluationQuestion)
                .Include(x => x.DialyActivity)
                .Where(x => x.DialyActivityId == id)
                .GroupBy(x => x.UserId)
        .Select(g => g.First())
                .ToListAsync();
            return list;
        }
    }
}
