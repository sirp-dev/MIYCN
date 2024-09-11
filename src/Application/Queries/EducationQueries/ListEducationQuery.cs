using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.EducationQueries
{
        public sealed class ListEducationQuery : IRequest<List<Education>>
    {
        public class ListEducationQueryHandler : IRequestHandler<ListEducationQuery, List<Education>>
        {
            private readonly IEducationRepository _repository;

            public ListEducationQueryHandler(IEducationRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Education>> Handle(ListEducationQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }

}
