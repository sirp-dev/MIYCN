using Application.Validators;
using Domain.DTOs;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.UserTestQueries
{
    public sealed class TestResultQuery : IRequest<UserTestResultDto>
    {
        public TestResultQuery(long trainingId, string userId, int testType)
        {
            TrainingId = trainingId;
            UserId = userId;
            TestType = testType;
        }

        public long TrainingId { get; }
        public string UserId { get; }
        public int TestType { get; }

        // Handler
        public class TestResultQueryHandler : IRequestHandler<TestResultQuery, UserTestResultDto>
        {

            private readonly IUserTestRepository _repository;

            public TestResultQueryHandler(IUserTestRepository repository)
            {
                _repository = repository;
            }
            public async Task<UserTestResultDto> Handle(TestResultQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                UserTestResultDto data = await _repository.UserTestResult(request.TrainingId, request.UserId, request.TestType);

                return data;
            }
        }
    }
}
