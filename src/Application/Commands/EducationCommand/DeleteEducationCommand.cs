using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EducationCommand
{
      public sealed class DeleteEducationCommand : IRequest
    {
        public DeleteEducationCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteEducationCommandHandler : IRequestHandler<DeleteEducationCommand>
    {
        private readonly IEducationRepository _repository;

        public DeleteEducationCommandHandler(IEducationRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
