using Application.Validators;
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
    public sealed class CheckUserTestByTrainingCommand : IRequest<bool>
    {
        public CheckUserTestByTrainingCommand(long trainingId, string userId, int testType)
        {
            TrainingId = trainingId;
            UserId = userId;
            TestType = testType;
        }

        public long TrainingId { get; }
        public string UserId { get; }
        public int TestType { get; }

        // Handler
        public class CheckUserTestByTrainingCommandHandler : IRequestHandler<CheckUserTestByTrainingCommand, bool>
        {

            private readonly IUserTestRepository _repository;

            public CheckUserTestByTrainingCommandHandler(IUserTestRepository repository)
            {
                _repository = repository;
            }
            public async Task<bool> Handle(CheckUserTestByTrainingCommand request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                bool data = await _repository.CheckIfUserTookTest(request.UserId, request.TrainingId, request.TestType);

                return data;
            }
        }
    }

}
