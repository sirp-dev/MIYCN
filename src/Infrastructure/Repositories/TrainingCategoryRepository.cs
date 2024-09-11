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
    public sealed class TrainingCategoryRepository : Repository<TrainingCategory>, ITrainingCategoryRepository
    {
        private readonly AppDbContext _context;

        public TrainingCategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<TrainingCategory>> GetAll()
        {
            return await _context.TrainingCategories
                .Include(x=>x.Training).ToListAsync();
        }
    }
}
