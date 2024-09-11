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
    public sealed class SponsorRepository : Repository<Sponsor>, ISponsorRepository
    {
        private readonly AppDbContext _context;

        public SponsorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Sponsor>> GetAll(long id)
        {
            var list = await _context.Sponsors.Where(x=>x.TrainingId == id).ToListAsync();
            return list;
        }
    }
}
