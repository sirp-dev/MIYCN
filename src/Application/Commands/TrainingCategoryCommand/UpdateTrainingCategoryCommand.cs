using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingCategoryCommand
{
     public sealed class UpdateTrainingCategoryCommand : IRequest
    {
        public UpdateTrainingCategoryCommand(TrainingCategory trainingCategory)
        {
            TrainingCategory = trainingCategory;
        }

        public TrainingCategory TrainingCategory { get; set; }


    }

    public class UpdateTrainingCategoryCommandHandler : IRequestHandler<UpdateTrainingCategoryCommand>
    {
        private readonly ITrainingCategoryRepository _repository;

        public UpdateTrainingCategoryCommandHandler(ITrainingCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateTrainingCategoryCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.TrainingCategory);
        }
    }
}
