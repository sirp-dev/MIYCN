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
    public sealed class ProfileCategoryRepository : Repository<ProfileCategory>, IProfileCategoryRepository
    {
        private readonly AppDbContext _context;

        public ProfileCategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ProfileCategory>> GetByUserId(string id)
        {
            return await _context.ProfileCategories.Include(x=>x.ProfileCategoryLists).Where(x=>x.AppUserId == id).ToListAsync();
        }
    }
}
