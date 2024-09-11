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
     public sealed class ModuleRepository : Repository<Module>, IModuleRepository
    {
        private readonly AppDbContext _context;

        public ModuleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Module>> GetAll()
        {
            var data = await _context.Modules
               .Include(x => x.Topic).ToListAsync();
            return data;
        }

        public async Task<Module> GetById(long id)
        {
            var data = await _context.Modules
                .Include(x=>x.Topic).FirstOrDefaultAsync(x=>x.Id == id);
            return data;
        }
    }
}
