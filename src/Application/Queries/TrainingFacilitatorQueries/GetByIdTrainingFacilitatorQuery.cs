using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TrainingFacilitatorQueries
{
    public sealed class GetByIdTrainingFacilitatorQuery : IRequest<TrainingFacilitator>
    {
        public GetByIdTrainingFacilitatorQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdTrainingFacilitatorQueryHandler : IRequestHandler<GetByIdTrainingFacilitatorQuery, TrainingFacilitator>
        {

            private readonly ITrainingFacilitatorRepository _repository;

            public GetByIdTrainingFacilitatorQueryHandler(ITrainingFacilitatorRepository repository)
            {
                _repository = repository;
            }
            public async Task<TrainingFacilitator> Handle(GetByIdTrainingFacilitatorQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                TrainingFacilitator data = await _repository.GetFacilitatorById(request.Id);

                return data;
            }
        }
    }

}
