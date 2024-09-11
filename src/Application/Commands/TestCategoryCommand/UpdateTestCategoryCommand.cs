using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TestCategoryCommand
{
     public sealed class UpdateTestCategoryCommand : IRequest
    {
        public UpdateTestCategoryCommand(TestCategory movie)
        {
            TestCategory = movie;
        }

        public TestCategory TestCategory { get; set; }


    }

    public class UpdateTestCategoryCommandHandler : IRequestHandler<UpdateTestCategoryCommand>
    {
        private readonly ITestCategoryRepository _repository;

        public UpdateTestCategoryCommandHandler(ITestCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateTestCategoryCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.TestCategory);
        }
    }
}
