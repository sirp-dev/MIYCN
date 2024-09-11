using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SettingCommand
{
    public sealed class AddSettingCommand : IRequest
    {
        public AddSettingCommand(Setting setting)
        {
            Setting = setting;
        }

        public Setting Setting { get; set; }


    }

    public class AddSettingCommandHandler : IRequestHandler<AddSettingCommand>
    {
        private readonly ISettingRepository _settingRepository;

        public AddSettingCommandHandler(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public async Task Handle(AddSettingCommand request, CancellationToken cancellationToken)
        {

            await _settingRepository.AddAsync(request.Setting);


        }
    }
}
