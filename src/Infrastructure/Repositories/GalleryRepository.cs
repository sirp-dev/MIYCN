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
    public sealed class GalleryRepository : Repository<Gallery>, IGalleryRepository
    {
        private readonly AppDbContext _context;

        public GalleryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Gallery>> GetAll(long trainingId)
        {
           var list = await _context.Galleries.Where(x=>x.TrainingId == trainingId).ToListAsync();
            return list;
        }
    }
}
