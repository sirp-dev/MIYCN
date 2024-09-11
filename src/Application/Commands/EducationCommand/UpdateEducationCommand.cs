using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EducationCommand
{
      public sealed class UpdateEducationCommand : IRequest
    {
        public UpdateEducationCommand(Education movie)
        {
            Education = movie;
        }

        public Education Education { get; set; }


    }

    public class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand>
    {
        private readonly IEducationRepository _repository;

        public UpdateEducationCommandHandler(IEducationRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.Education);
        }
    }
}
