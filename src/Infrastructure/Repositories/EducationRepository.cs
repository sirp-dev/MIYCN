using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public sealed class EducationRepository : Repository<Education>, IEducationRepository
    {
        private readonly AppDbContext _context;

        public EducationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
