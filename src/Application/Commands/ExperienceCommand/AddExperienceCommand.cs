using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ExperienceCommand
{
     public sealed class AddExperienceCommand : IRequest
    {
        public AddExperienceCommand(Experience experience)
        {
            Experience = experience;
        }

        public Experience Experience { get; set; }


    }

    public class AddExperienceCommandHandler : IRequestHandler<AddExperienceCommand>
    {
        private readonly IExperienceRepository _experienceRepository;

        public AddExperienceCommandHandler(IExperienceRepository experienceRepository)
        {
            _experienceRepository = experienceRepository;
        }

        public async Task Handle(AddExperienceCommand request, CancellationToken cancellationToken)
        {

            await _experienceRepository.AddAsync(request.Experience);


        }
    }
}
