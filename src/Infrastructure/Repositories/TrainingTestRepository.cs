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

    public sealed class TrainingTestRepository : Repository<TrainingTest>, ITrainingTestRepository
    {
        private readonly AppDbContext _context;

        public TrainingTestRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
