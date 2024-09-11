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
        public sealed class ListTrainingQuery : IRequest<List<Training>>
    {

        public string State { get; set; }

        public ListTrainingQuery(string state)
        {
            State = state;
        }

        public class ListTrainingQueryHandler : IRequestHandler<ListTrainingQuery, List<Training>>
        {
            private readonly ITrainingRepository _repository;

            public ListTrainingQueryHandler(ITrainingRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Training>> Handle(ListTrainingQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAll(request.State);

            }
        }
    }

}
