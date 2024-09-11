using Application.Validators;
using Domain.DTOs;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TrainingQueries
{
    public sealed class GetTrainingReportQuery : IRequest<List<TrainingDto>>
    {
        

        // Handler
        public class GetTrainingReportQueryHandler : IRequestHandler<GetTrainingReportQuery, List<TrainingDto>>
        {

            private readonly ITrainingRepository _repository;

            public GetTrainingReportQueryHandler(ITrainingRepository repository)
            {
                _repository = repository;
            }
            public async Task<List<TrainingDto>> Handle(GetTrainingReportQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                var data = await _repository.GetTrainingByReportList();

                return data;
            }
        }
    }
}
