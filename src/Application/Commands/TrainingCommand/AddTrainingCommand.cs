using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingCommand
{
    public sealed class AddTrainingCommand : IRequest
    {
        public AddTrainingCommand(Training training)
        {
            Training = training;
        }

        public Training Training { get; set; }


    }

    public class AddTrainingCommandHandler : IRequestHandler<AddTrainingCommand>
    {
        private readonly ITrainingRepository _trainingRepository;

        public AddTrainingCommandHandler(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public async Task Handle(AddTrainingCommand request, CancellationToken cancellationToken)
        {

            await _trainingRepository.AddAsync(request.Training);


        }
    }
}
