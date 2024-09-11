using Application.Validators;
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
    public sealed class GetByIdModuleTopicQuery : IRequest<ModuleTopic>
    {
        public GetByIdModuleTopicQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdModuleTopicQueryHandler : IRequestHandler<GetByIdModuleTopicQuery, ModuleTopic>
        {

            private readonly IModuleTopicRepository _repository;

            public GetByIdModuleTopicQueryHandler(IModuleTopicRepository repository)
            {
                _repository = repository;
            }
            public async Task<ModuleTopic> Handle(GetByIdModuleTopicQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                ModuleTopic data = await _repository.GetById(request.Id);

                return data;
            }
        }
    }

}
