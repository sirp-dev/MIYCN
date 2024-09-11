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
      public sealed class UpdateTestSheetCommand : IRequest
    {
        public UpdateTestSheetCommand(TestSheet movie)
        {
            TestSheet = movie;
        }

        public TestSheet TestSheet { get; set; }


    }

    public class UpdateTestSheetCommandHandler : IRequestHandler<UpdateTestSheetCommand>
    {
        private readonly ITestSheetRepository _repository;

        public UpdateTestSheetCommandHandler(ITestSheetRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateTestSheetCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.TestSheet);
        }
    }
}
