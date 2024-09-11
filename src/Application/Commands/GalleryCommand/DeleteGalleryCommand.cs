using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.GalleryCommand
{
      public sealed class DeleteGalleryCommand : IRequest
    {
        public DeleteGalleryCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteGalleryCommandHandler : IRequestHandler<DeleteGalleryCommand>
    {
        private readonly IGalleryRepository _repository;

        public DeleteGalleryCommandHandler(IGalleryRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteGalleryCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
