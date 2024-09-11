using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ProfileCategoryCommand
{
     public sealed class DeleteProfileCategoryCommand : IRequest
    {
        public DeleteProfileCategoryCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteProfileCategoryCommandHandler : IRequestHandler<DeleteProfileCategoryCommand>
    {
        private readonly IProfileCategoryRepository _repository;

        public DeleteProfileCategoryCommandHandler(IProfileCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteProfileCategoryCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
