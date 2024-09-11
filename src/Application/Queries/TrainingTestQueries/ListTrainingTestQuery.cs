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
    public sealed class ListTrainingTestQuery : IRequest<List<TrainingTest>>
    {
        public class ListTrainingTestQueryHandler : IRequestHandler<ListTrainingTestQuery, List<TrainingTest>>
        {
            private readonly ITrainingTestRepository _repository;

            public ListTrainingTestQueryHandler(ITrainingTestRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<TrainingTest>> Handle(ListTrainingTestQuery request, CancellationToken cancellationToken)
            {
                var list = await _repository.GetAllAsync();
                return list.Where(x=>x.Publish == true).ToList();
            }
        }
    }

}
