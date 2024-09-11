using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.ModuleTopicQueries
{
    public sealed class ListModuleTopicQuery : IRequest<List<ModuleTopic>>
    {
        public class ListModuleTopicQueryHandler : IRequestHandler<ListModuleTopicQuery, List<ModuleTopic>>
        {
            private readonly IModuleTopicRepository _repository;

            public ListModuleTopicQueryHandler(IModuleTopicRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<ModuleTopic>> Handle(ListModuleTopicQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAll();

            }
        }
    }

}
