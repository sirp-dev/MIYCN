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

namespace Infrastructure.Repositories
{
    public sealed class DialyActivityRepository : Repository<DialyActivity>, IDialyActivityRepository
    {
        private readonly AppDbContext _context;

        public DialyActivityRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<DialyActivity>> GetActivityByTrainingId(long trainingId)
        {
            
            var list = await _context.DialyActivities
                .Include(x => x.Attendances)
                .Include(x => x.Training)
                .Where(x=>x.TrainingId == trainingId).ToListAsync();
            return list;
        }

        public async Task<DialyActivity> GetActivityByTrainingIdAndDate(long trainingId, DateTime date)
        {
            var data = await _context.DialyActivities
                .Include(x=>x.Attendances)
                .Include(x=>x.Training)
                .FirstOrDefaultAsync(x => x.TrainingId == trainingId && x.Date.Date == date.Date);
            return data;
        }
    }
}
