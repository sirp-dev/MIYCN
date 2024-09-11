using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TrainingTestQueries
{
    public sealed class GetByIdTrainingTestQuery : IRequest<TrainingTest>
    {
        public GetByIdTrainingTestQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdTrainingTestQueryHandler : IRequestHandler<GetByIdTrainingTestQuery, TrainingTest>
        {

            private readonly ITrainingTestRepository _repository;

            public GetByIdTrainingTestQueryHandler(ITrainingTestRepository repository)
            {
                _repository = repository;
            }
            public async Task<TrainingTest> Handle(GetByIdTrainingTestQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                TrainingTest data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
