using Application.Validators;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TrainingQueries
{
    public sealed class GetByIdAndCountTrainingQuery : IRequest<TrainingDto>
    {
        public GetByIdAndCountTrainingQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdAndCountTrainingQueryHandler : IRequestHandler<GetByIdAndCountTrainingQuery, TrainingDto>
        {

            private readonly ITrainingRepository _repository;

            public GetByIdAndCountTrainingQueryHandler(ITrainingRepository repository)
            {
                _repository = repository;
            }
            public async Task<TrainingDto> Handle(GetByIdAndCountTrainingQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                TrainingDto data = await _repository.GetTrainingByIdAndCounts(request.Id);

                return data;
            }
        }
    }

}
