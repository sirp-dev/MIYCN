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
     public sealed class AddTrainingCategoryCommand : IRequest
    {
        public AddTrainingCategoryCommand(TrainingCategory trainingCategory)
        {
            TrainingCategory = trainingCategory;
        }

        public TrainingCategory TrainingCategory { get; set; }


    }

    public class AddTrainingCategoryCommandHandler : IRequestHandler<AddTrainingCategoryCommand>
    {
        private readonly ITrainingCategoryRepository _trainingCategoryRepository;

        public AddTrainingCategoryCommandHandler(ITrainingCategoryRepository trainingCategoryRepository)
        {
            _trainingCategoryRepository = trainingCategoryRepository;
        }

        public async Task Handle(AddTrainingCategoryCommand request, CancellationToken cancellationToken)
        {

            await _trainingCategoryRepository.AddAsync(request.TrainingCategory);


        }
    }
}
