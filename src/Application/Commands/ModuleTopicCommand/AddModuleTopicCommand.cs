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
    public sealed class AddModuleTopicCommand : IRequest
    {
        public AddModuleTopicCommand(ModuleTopic moduleTopic)
        {
            ModuleTopic = moduleTopic;
        }

        public ModuleTopic ModuleTopic { get; set; }


    }

    public class AddModuleTopicCommandHandler : IRequestHandler<AddModuleTopicCommand>
    {
        private readonly IModuleTopicRepository _moduleTopicRepository;

        public AddModuleTopicCommandHandler(IModuleTopicRepository moduleTopicRepository)
        {
            _moduleTopicRepository = moduleTopicRepository;
        }

        public async Task Handle(AddModuleTopicCommand request, CancellationToken cancellationToken)
        {

            await _moduleTopicRepository.AddAsync(request.ModuleTopic);


        }
    }
}
