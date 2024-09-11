using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TestSheetCommand
{
     public sealed class DeleteTestSheetCommand : IRequest
    {
        public DeleteTestSheetCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteTestSheetCommandHandler : IRequestHandler<DeleteTestSheetCommand>
    {
        private readonly ITestSheetRepository _repository;

        public DeleteTestSheetCommandHandler(ITestSheetRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteTestSheetCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
