using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TestSheetQueries
{
        public sealed class ListTestSheetQuery : IRequest<List<TestSheet>>
    {
        public class ListTestSheetQueryHandler : IRequestHandler<ListTestSheetQuery, List<TestSheet>>
        {
            private readonly ITestSheetRepository _repository;

            public ListTestSheetQueryHandler(ITestSheetRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<TestSheet>> Handle(ListTestSheetQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }

}
