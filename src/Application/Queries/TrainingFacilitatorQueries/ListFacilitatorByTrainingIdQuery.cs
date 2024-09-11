using Domain.DTOs;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TrainingFacilitatorQueries
{
    public sealed class ListFacilitatorByTrainingIdQuery : IRequest<List<FacilitatorInTrainingDTo>>
    {

        public ListFacilitatorByTrainingIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        public class ListFacilitatorByTrainingIdQueryHandler : IRequestHandler<ListFacilitatorByTrainingIdQuery, List<FacilitatorInTrainingDTo>>
        {
            private readonly ITrainingFacilitatorRepository _repository;

            public ListFacilitatorByTrainingIdQueryHandler(ITrainingFacilitatorRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<FacilitatorInTrainingDTo>> Handle(ListFacilitatorByTrainingIdQuery request, CancellationToken cancellationToken)
            {
                return await _repository.FacilitatorInTraining(request.Id);

            }
        }
    }

}
