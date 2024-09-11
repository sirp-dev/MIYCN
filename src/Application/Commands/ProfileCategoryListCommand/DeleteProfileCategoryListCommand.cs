using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ProfileCategoryListCommand
{
    public sealed class DeleteProfileCategoryListCommand : IRequest
    {
        public DeleteProfileCategoryListCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteProfileCategoryListCommandHandler : IRequestHandler<DeleteProfileCategoryListCommand>
    {
        private readonly IProfileCategoryListRepository _repository;

        public DeleteProfileCategoryListCommandHandler(IProfileCategoryListRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteProfileCategoryListCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
