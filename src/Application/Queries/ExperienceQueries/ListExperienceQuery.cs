using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.ExperienceQueries
{
        public sealed class ListExperienceQuery : IRequest<List<Experience>>
    {
        public class ListExperienceQueryHandler : IRequestHandler<ListExperienceQuery, List<Experience>>
        {
            private readonly IExperienceRepository _repository;

            public ListExperienceQueryHandler(IExperienceRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Experience>> Handle(ListExperienceQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }

}
