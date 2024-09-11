using Application.Validators;
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
    public sealed class GetByIdTrainingQuery : IRequest<Training>
    {
        public GetByIdTrainingQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdTrainingQueryHandler : IRequestHandler<GetByIdTrainingQuery, Training>
        {

            private readonly ITrainingRepository _repository;

            public GetByIdTrainingQueryHandler(ITrainingRepository repository)
            {
                _repository = repository;
            }
            public async Task<Training> Handle(GetByIdTrainingQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                Training data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
