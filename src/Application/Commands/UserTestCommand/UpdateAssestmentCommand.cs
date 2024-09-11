using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Application.Commands.UserTestCommand
{
    public sealed class UpdateAssestmentCommand : IRequest
    {
        public UpdateAssestmentCommand(List<(long questionId, int answer, string userId, long trainingId)> testeData)
        {
            TesteData = testeData;
        }

        public List<(long questionId, int answer, string userId, long trainingId)> TesteData { get; set; }
    }

    public class UpdateAssestmentCommandHandler : IRequestHandler<UpdateAssestmentCommand>
    {
        private readonly IUserTestRepository _repository;

        public UpdateAssestmentCommandHandler(IUserTestRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateAssestmentCommand request, CancellationToken cancellationToken)
        {
            await _repository.UserTestSubmit(request.TesteData);
        }
    }

}
