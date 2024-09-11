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
     public sealed class AddEducationCommand : IRequest
    {
        public AddEducationCommand(Education education)
        {
            Education = education;
        }

        public Education Education { get; set; }


    }

    public class AddEducationCommandHandler : IRequestHandler<AddEducationCommand>
    {
        private readonly IEducationRepository _educationRepository;

        public AddEducationCommandHandler(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        public async Task Handle(AddEducationCommand request, CancellationToken cancellationToken)
        {

            await _educationRepository.AddAsync(request.Education);


        }
    }
}
