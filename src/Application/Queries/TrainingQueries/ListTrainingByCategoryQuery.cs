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
        public sealed class ListTrainingByCategoryQuery : IRequest<List<Training>>
    {

        public string State { get; set; }
        public long Id { get; set; }

        public ListTrainingByCategoryQuery(string state, long id)
        {
            State = state;
            Id = id;
        }

        public class ListTrainingByCategoryQueryHandler : IRequestHandler<ListTrainingByCategoryQuery, List<Training>>
        {
            private readonly ITrainingRepository _repository;

            public ListTrainingByCategoryQueryHandler(ITrainingRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Training>> Handle(ListTrainingByCategoryQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllByCategoryId(request.State, request.Id);

            }
        }
    }

}
