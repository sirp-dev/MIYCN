using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SponsorCommand
{
      public sealed class DeleteSponsorCommand : IRequest
    {
        public DeleteSponsorCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteSponsorCommandHandler : IRequestHandler<DeleteSponsorCommand>
    {
        private readonly ISponsorRepository _repository;

        public DeleteSponsorCommandHandler(ISponsorRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteSponsorCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
