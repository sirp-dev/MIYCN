using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingTestCommand
{
    public sealed class AddTrainingTestCommand : IRequest
    {
        public AddTrainingTestCommand(TrainingTest trainingTest)
        {
            TrainingTest = trainingTest;
        }

        public TrainingTest TrainingTest { get; set; }


    }

    public class AddTrainingTestCommandHandler : IRequestHandler<AddTrainingTestCommand>
    {
        private readonly ITrainingTestRepository _TrainingTestRepository;

        public AddTrainingTestCommandHandler(ITrainingTestRepository TrainingTestRepository)
        {
            _TrainingTestRepository = TrainingTestRepository;
        }

        public async Task Handle(AddTrainingTestCommand request, CancellationToken cancellationToken)
        {

            await _TrainingTestRepository.AddAsync(request.TrainingTest);


        }
    }
}
