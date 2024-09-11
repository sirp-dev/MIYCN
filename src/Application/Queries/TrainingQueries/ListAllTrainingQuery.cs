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
    
    public sealed class ListAllTrainingQuery : IRequest<IQueryable<Training>>
    {

        public class ListAllTrainingQueryHandler : IRequestHandler<ListAllTrainingQuery, IQueryable<Training>>
        {
            private readonly ITrainingRepository _repository;

            public ListAllTrainingQueryHandler(ITrainingRepository repository)
            {
                _repository = repository;
            }

            public async Task<IQueryable<Training>> Handle(ListAllTrainingQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllTrainingWithDetails();

            }
        }
    }

}
