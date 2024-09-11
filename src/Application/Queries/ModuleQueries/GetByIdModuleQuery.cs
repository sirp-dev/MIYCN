using Application.Validators;
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
    public sealed class GetByIdModuleQuery : IRequest<Module>
    {
        public GetByIdModuleQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdModuleQueryHandler : IRequestHandler<GetByIdModuleQuery, Module>
        {

            private readonly IModuleRepository _repository;

            public GetByIdModuleQueryHandler(IModuleRepository repository)
            {
                _repository = repository;
            }
            public async Task<Module> Handle(GetByIdModuleQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                Module data = await _repository.GetById(request.Id);

                return data;
            }
        }
    }

}
