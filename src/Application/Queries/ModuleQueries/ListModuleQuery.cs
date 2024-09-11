using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.ModuleQueries
{
    public sealed class ListModuleQuery : IRequest<List<Module>>
    {
        public class ListModuleQueryHandler : IRequestHandler<ListModuleQuery, List<Module>>
        {
            private readonly IModuleRepository _repository;

            public ListModuleQueryHandler(IModuleRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Module>> Handle(ListModuleQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAll();

            }
        }
    }

}
