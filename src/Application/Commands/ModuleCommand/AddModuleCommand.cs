using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ModuleCommand
{
     public sealed class AddModuleCommand : IRequest
    {
        public AddModuleCommand(Module module)
        {
            Module = module;
        }

        public Module Module { get; set; }


    }

    public class AddModuleCommandHandler : IRequestHandler<AddModuleCommand>
    {
        private readonly IModuleRepository _moduleRepository;

        public AddModuleCommandHandler(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public async Task Handle(AddModuleCommand request, CancellationToken cancellationToken)
        {

            await _moduleRepository.AddAsync(request.Module);


        }
    }
}
