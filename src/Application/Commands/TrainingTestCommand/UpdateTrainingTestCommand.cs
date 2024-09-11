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
     public sealed class UpdateTrainingTestCommand : IRequest
    {
        public UpdateTrainingTestCommand(TrainingTest movie)
        {
            TrainingTest = movie;
        }

        public TrainingTest TrainingTest { get; set; }


    }

    public class UpdateTrainingTestCommandHandler : IRequestHandler<UpdateTrainingTestCommand>
    {
        private readonly ITrainingTestRepository _repository;

        public UpdateTrainingTestCommandHandler(ITrainingTestRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateTrainingTestCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.TrainingTest);
        }
    }
}
