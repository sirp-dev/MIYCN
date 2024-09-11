using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TestCategoryQueries
{
        public sealed class ListTestCategoryQuery : IRequest<List<TestCategory>>
    {
        public class ListTestCategoryQueryHandler : IRequestHandler<ListTestCategoryQuery, List<TestCategory>>
        {
            private readonly ITestCategoryRepository _repository;

            public ListTestCategoryQueryHandler(ITestCategoryRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<TestCategory>> Handle(ListTestCategoryQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }

}
