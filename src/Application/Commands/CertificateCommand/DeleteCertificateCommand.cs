using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CertificateCommand
{
    public sealed class DeleteCertificateCommand : IRequest
    {
        public DeleteCertificateCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteCertificateCommandHandler : IRequestHandler<DeleteCertificateCommand>
    {
        private readonly ICertificateRepository _repository;

        public DeleteCertificateCommandHandler(ICertificateRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteCertificateCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
