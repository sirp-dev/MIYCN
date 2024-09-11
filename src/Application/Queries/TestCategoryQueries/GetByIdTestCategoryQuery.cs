using Application.Validators;
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
         public sealed class GetByIdTestCategoryQuery : IRequest<TestCategory>
    {
        public GetByIdTestCategoryQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdTestCategoryQueryHandler : IRequestHandler<GetByIdTestCategoryQuery, TestCategory>
        {

            private readonly ITestCategoryRepository _repository;

            public GetByIdTestCategoryQueryHandler(ITestCategoryRepository repository)
            {
                _repository = repository;
            }
            public async Task<TestCategory> Handle(GetByIdTestCategoryQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                TestCategory data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
