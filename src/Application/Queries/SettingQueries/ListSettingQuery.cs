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
    public sealed class ListSettingQuery : IRequest<List<Setting>>
    {
        public class ListSettingQueryHandler : IRequestHandler<ListSettingQuery, List<Setting>>
        {
            private readonly ISettingRepository _repository;

            public ListSettingQueryHandler(ISettingRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Setting>> Handle(ListSettingQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }
}
