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
    public sealed class AddDialyActivityCommand : IRequest
    {
        public AddDialyActivityCommand(DialyActivity dailyActivity)
        {
            DialyActivity = dailyActivity;
        }

        public DialyActivity DialyActivity { get; set; }


    }

    public class AddDialyActivityCommandHandler : IRequestHandler<AddDialyActivityCommand>
    {
        private readonly IDialyActivityRepository _dailyActivityRepository;

        public AddDialyActivityCommandHandler(IDialyActivityRepository dailyActivityRepository)
        {
            _dailyActivityRepository = dailyActivityRepository;
        }

        public async Task Handle(AddDialyActivityCommand request, CancellationToken cancellationToken)
        {
            var check = await _dailyActivityRepository.GetActivityByTrainingIdAndDate(request.DialyActivity.TrainingId, request.DialyActivity.Date);
            if (check == null)
            {
                await _dailyActivityRepository.AddAsync(request.DialyActivity);
            }

        }
    }
}
