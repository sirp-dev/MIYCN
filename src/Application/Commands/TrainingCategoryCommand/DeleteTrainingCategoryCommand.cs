using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingCategoryCommand
{
    public sealed class DeleteTrainingCategoryCommand : IRequest
    {
        public DeleteTrainingCategoryCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteTrainingCategoryCommandHandler : IRequestHandler<DeleteTrainingCategoryCommand>
    {
        private readonly ITrainingCategoryRepository _repository;

        public DeleteTrainingCategoryCommandHandler(ITrainingCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteTrainingCategoryCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
