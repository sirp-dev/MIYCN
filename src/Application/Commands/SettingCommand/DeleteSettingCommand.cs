using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SettingCommand
{
    public sealed class DeleteSettingCommand : IRequest
    {
        public DeleteSettingCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteSettingCommandHandler : IRequestHandler<DeleteSettingCommand>
    {
        private readonly ISettingRepository _repository;

        public DeleteSettingCommandHandler(ISettingRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteSettingCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
