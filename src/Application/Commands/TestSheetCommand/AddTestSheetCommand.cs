using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TestSheetCommand
{
     public sealed class AddTestSheetCommand : IRequest
    {
        public AddTestSheetCommand(TestSheet testSheet)
        {
            TestSheet = testSheet;
        }

        public TestSheet TestSheet { get; set; }


    }

    public class AddTestSheetCommandHandler : IRequestHandler<AddTestSheetCommand>
    {
        private readonly ITestSheetRepository _testSheetRepository;

        public AddTestSheetCommandHandler(ITestSheetRepository testSheetRepository)
        {
            _testSheetRepository = testSheetRepository;
        }

        public async Task Handle(AddTestSheetCommand request, CancellationToken cancellationToken)
        {

            await _testSheetRepository.AddAsync(request.TestSheet);


        }
    }
}
