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
     public sealed class UpdateExperienceCommand : IRequest
    {
        public UpdateExperienceCommand(Experience movie)
        {
            Experience = movie;
        }

        public Experience Experience { get; set; }


    }

    public class UpdateExperienceCommandHandler : IRequestHandler<UpdateExperienceCommand>
    {
        private readonly IExperienceRepository _repository;

        public UpdateExperienceCommandHandler(IExperienceRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.Experience);
        }
    }
}
