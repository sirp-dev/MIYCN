using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.DialyActivityCommand
{
     public sealed class DeleteDialyActivityCommand : IRequest
    {
        public DeleteDialyActivityCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteDialyActivityCommandHandler : IRequestHandler<DeleteDialyActivityCommand>
    {
        private readonly IDialyActivityRepository _repository;

        public DeleteDialyActivityCommandHandler(IDialyActivityRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteDialyActivityCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
