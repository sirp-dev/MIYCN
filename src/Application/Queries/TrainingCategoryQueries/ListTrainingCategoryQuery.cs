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
        public sealed class ListTrainingCategoryQuery : IRequest<List<TrainingCategory>>
    {
        public class ListTrainingCategoryQueryHandler : IRequestHandler<ListTrainingCategoryQuery, List<TrainingCategory>>
        {
            private readonly ITrainingCategoryRepository _repository;

            public ListTrainingCategoryQueryHandler(ITrainingCategoryRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<TrainingCategory>> Handle(ListTrainingCategoryQuery request, CancellationToken cancellationToken)
            {
                var list = await _repository.GetAll();
                return list.ToList();
            }
        }
    }

}
