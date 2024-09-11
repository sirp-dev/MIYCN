using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TestCategoryCommand
{
      public sealed class DeleteTestCategoryCommand : IRequest
    {
        public DeleteTestCategoryCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteTestCategoryCommandHandler : IRequestHandler<DeleteTestCategoryCommand>
    {
        private readonly ITestCategoryRepository _repository;

        public DeleteTestCategoryCommandHandler(ITestCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteTestCategoryCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
