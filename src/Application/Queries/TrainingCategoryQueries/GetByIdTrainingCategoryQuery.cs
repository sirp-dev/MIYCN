using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TrainingCategoryQueries
{
        public sealed class GetByIdTrainingCategoryQuery : IRequest<TrainingCategory>
    {
        public GetByIdTrainingCategoryQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdTrainingCategoryQueryHandler : IRequestHandler<GetByIdTrainingCategoryQuery, TrainingCategory>
        {

            private readonly ITrainingCategoryRepository _repository;

            public GetByIdTrainingCategoryQueryHandler(ITrainingCategoryRepository repository)
            {
                _repository = repository;
            }
            public async Task<TrainingCategory> Handle(GetByIdTrainingCategoryQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                TrainingCategory data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
