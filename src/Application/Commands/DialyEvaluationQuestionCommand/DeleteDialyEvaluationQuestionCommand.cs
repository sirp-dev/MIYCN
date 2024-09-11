using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.DialyEvaluationQuestionCommand
{
    public sealed class DeleteDialyEvaluationQuestionCommand : IRequest<bool>
    {
        public DeleteDialyEvaluationQuestionCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteDialyEvaluationQuestionCommandHandler : IRequestHandler<DeleteDialyEvaluationQuestionCommand, bool>
    {
        private readonly IDialyEvaluationQuestionRepository _repository;
        private readonly IDialyUserEvaluationRepository _dailyrepository;

        public DeleteDialyEvaluationQuestionCommandHandler(IDialyEvaluationQuestionRepository repository, IDialyUserEvaluationRepository dailyrepository)
        {
            _repository = repository;
            _dailyrepository = dailyrepository;
        }

        public async Task<bool> Handle(DeleteDialyEvaluationQuestionCommand request, CancellationToken cancellationToken)
        {
            var check = await _dailyrepository.CheckIfEvaluationHasBeenTaken(request.Id);
            if (check == false)
            {
                var movie = await _repository.GetByIdAsync(request.Id);

                await _repository.RemoveAsync(movie);
                return true;
            }
           
            return false;
        }
    }
}
