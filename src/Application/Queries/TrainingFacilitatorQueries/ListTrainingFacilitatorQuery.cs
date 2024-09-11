using Domain.DTOs;
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
         public sealed class ListTrainingFacilitatorQuery : IRequest<List<FacilitatorInTrainingDTo>>
    {
        public class ListTrainingFacilitatorQueryHandler : IRequestHandler<ListTrainingFacilitatorQuery, List<FacilitatorInTrainingDTo>>
        {
            private readonly ITrainingFacilitatorRepository _repository;

            public ListTrainingFacilitatorQueryHandler(ITrainingFacilitatorRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<FacilitatorInTrainingDTo>> Handle(ListTrainingFacilitatorQuery request, CancellationToken cancellationToken)
            {
                return await _repository.AllFacilitator();

            }
        }
    }

}
