using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.UserTestQueries
{
    public sealed class ListUserTestByTrainingQuery : IRequest<List<UserTestListDto>>
    {


        public long TrainingId { get; set; }

        public ListUserTestByTrainingQuery(long trainingId)
        {
            TrainingId = trainingId;
        }

        public class ListUserTestByTrainingQueryHandler : IRequestHandler<ListUserTestByTrainingQuery, List<UserTestListDto>>
        {
            private readonly IUserTestRepository _repository;

            public ListUserTestByTrainingQueryHandler(IUserTestRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<UserTestListDto>> Handle(ListUserTestByTrainingQuery request, CancellationToken cancellationToken)
            {
                return await _repository.ListUserTestByTrainingId(request.TrainingId);

            }
        }
    }

}
