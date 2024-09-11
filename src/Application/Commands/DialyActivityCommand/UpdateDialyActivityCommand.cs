using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.DialyActivityCommand
{
     public sealed class UpdateDialyActivityCommand : IRequest
    {
        public UpdateDialyActivityCommand(DialyActivity movie)
        {
            DialyActivity = movie;
        }

        public DialyActivity DialyActivity { get; set; }


    }

    public class UpdateDialyActivityCommandHandler : IRequestHandler<UpdateDialyActivityCommand>
    {
        private readonly IDialyActivityRepository _repository;

        public UpdateDialyActivityCommandHandler(IDialyActivityRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateDialyActivityCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.DialyActivity);
        }
    }
}
