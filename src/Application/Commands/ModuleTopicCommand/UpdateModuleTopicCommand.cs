using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ModuleTopicCommand
{
     public sealed class UpdateModuleTopicCommand : IRequest
    {
        public UpdateModuleTopicCommand(ModuleTopic movie)
        {
            ModuleTopic = movie;
        }

        public ModuleTopic ModuleTopic { get; set; }


    }

    public class UpdateModuleTopicCommandHandler : IRequestHandler<UpdateModuleTopicCommand>
    {
        private readonly IModuleTopicRepository _repository;

        public UpdateModuleTopicCommandHandler(IModuleTopicRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateModuleTopicCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.ModuleTopic);
        }
    }
}
