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
     public sealed class TimeTableRepository : Repository<TimeTable>, ITimeTableRepository
    {
        private readonly AppDbContext _context;

        public TimeTableRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<TimeTable>> GetAll(long trainingId)
        {
           var list = await _context.TimeTables
                .Include(x=>x.Facilitator)
                .Include(x=>x.Training)
                .Include(x=>x.ModuleTopic).ThenInclude(x=>x.Module)
                .Where(x=>x.TrainingId == trainingId).ToListAsync();
            return list;
        }
    }
}
