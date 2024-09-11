using Application.Validators;
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
      public sealed class GetByIdTestSheetQuery : IRequest<TestSheet>
    {
        public GetByIdTestSheetQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdTestSheetQueryHandler : IRequestHandler<GetByIdTestSheetQuery, TestSheet>
        {

            private readonly ITestSheetRepository _repository;

            public GetByIdTestSheetQueryHandler(ITestSheetRepository repository)
            {
                _repository = repository;
            }
            public async Task<TestSheet> Handle(GetByIdTestSheetQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                TestSheet data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
