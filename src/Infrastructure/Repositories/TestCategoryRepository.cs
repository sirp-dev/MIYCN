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
    public sealed class TestCategoryRepository : Repository<TestCategory>, ITestCategoryRepository
    {
        private readonly AppDbContext _context;

        public TestCategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
