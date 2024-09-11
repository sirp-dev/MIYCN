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
    public sealed class ModuleTopicRepository : Repository<ModuleTopic>, IModuleTopicRepository
    {
        private readonly AppDbContext _context;

        public ModuleTopicRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ModuleTopic>> GetAll()
        {
           var list = await _context.ModuleTopics.Include(x=>x.Module).ToListAsync();
            return list;
        }

        public async Task<ModuleTopic> GetById(long id)
        {
            var data = await _context.ModuleTopics.Include(x => x.Module).FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }
    }
}
