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
     public sealed class UpdateModuleCommand : IRequest
    {
        public UpdateModuleCommand(Module movie)
        {
            Module = movie;
        }

        public Module Module { get; set; }


    }

    public class UpdateModuleCommandHandler : IRequestHandler<UpdateModuleCommand>
    {
        private readonly IModuleRepository _repository;

        public UpdateModuleCommandHandler(IModuleRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.Module);
        }
    }
}
