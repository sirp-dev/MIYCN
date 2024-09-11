using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.SettingQueries
{
    public sealed class GetByIdSettingQuery : IRequest<Setting>
    {
        public GetByIdSettingQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdSettingQueryHandler : IRequestHandler<GetByIdSettingQuery, Setting>
        {

            private readonly ISettingRepository _repository;

            public GetByIdSettingQueryHandler(ISettingRepository repository)
            {
                _repository = repository;
            }
            public async Task<Setting> Handle(GetByIdSettingQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                Setting data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }
}
