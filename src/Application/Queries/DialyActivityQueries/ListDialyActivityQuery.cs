using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.DialyActivityQueries
{
    public sealed class ListDialyActivityQuery : IRequest<List<DialyActivity>>
    {
        public class ListDialyActivityQueryHandler : IRequestHandler<ListDialyActivityQuery, List<DialyActivity>>
        {
            private readonly IDialyActivityRepository _repository;

            public ListDialyActivityQueryHandler(IDialyActivityRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<DialyActivity>> Handle(ListDialyActivityQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }

}
