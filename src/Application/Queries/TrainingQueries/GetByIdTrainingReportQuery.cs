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
        public sealed class GetByIdTrainingReportQuery : IRequest<TrainingDto>
    {
        public GetByIdTrainingReportQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdTrainingReportQueryHandler : IRequestHandler<GetByIdTrainingReportQuery, TrainingDto>
        {

            private readonly ITrainingRepository _repository;

            public GetByIdTrainingReportQueryHandler(ITrainingRepository repository)
            {
                _repository = repository;
            }
            public async Task<TrainingDto> Handle(GetByIdTrainingReportQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                TrainingDto data = await _repository.GetTrainingByIdReport(request.Id);

                return data;
            }
        }
    }

}
