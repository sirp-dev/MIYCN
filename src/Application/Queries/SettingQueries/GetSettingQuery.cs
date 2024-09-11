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
    public sealed class GetSettingQuery : IRequest<Setting>
    {
        public long TrainingId { get; set; }

        public GetSettingQuery(long trainingId)
        {
            TrainingId = trainingId;
        }

        // Handler
        public class GetSettingQueryHandler : IRequestHandler<GetSettingQuery, Setting>
        {

            private readonly ISettingRepository _repository;

            public GetSettingQueryHandler(ISettingRepository repository)
            {
                _repository = repository;
            }
            public async Task<Setting> Handle(GetSettingQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                Setting data = await _repository.GetSetting(request.TrainingId);

                return data;
            }
        }
    }

}
