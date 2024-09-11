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
    public sealed class AddTestCategoryCommand : IRequest
    {
        public AddTestCategoryCommand(TestCategory testCategory)
        {
            TestCategory = testCategory;
        }

        public TestCategory TestCategory { get; set; }


    }

    public class AddTestCategoryCommandHandler : IRequestHandler<AddTestCategoryCommand>
    {
        private readonly ITestCategoryRepository _testCategoryRepository;

        public AddTestCategoryCommandHandler(ITestCategoryRepository testCategoryRepository)
        {
            _testCategoryRepository = testCategoryRepository;
        }

        public async Task Handle(AddTestCategoryCommand request, CancellationToken cancellationToken)
        {

            await _testCategoryRepository.AddAsync(request.TestCategory);


        }
    }
}
