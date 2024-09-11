using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingFacilitatorCommand
{
     public sealed class UpdateTrainingFacilitatorCommand : IRequest
    {
        public UpdateTrainingFacilitatorCommand(TrainingFacilitator movie)
        {
            TrainingFacilitator = movie;
        }

        public TrainingFacilitator TrainingFacilitator { get; set; }


    }

    public class UpdateTrainingFacilitatorCommandHandler : IRequestHandler<UpdateTrainingFacilitatorCommand>
    {
        private readonly ITrainingFacilitatorRepository _repository;

        public UpdateTrainingFacilitatorCommandHandler(ITrainingFacilitatorRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateTrainingFacilitatorCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.TrainingFacilitator);
        }
    }
}
