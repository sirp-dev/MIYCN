using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EvaluationQuestionCategoryCommand
{
     public sealed class AddEvaluationQuestionCategoryCommand : IRequest
    {
        public AddEvaluationQuestionCategoryCommand(EvaluationQuestionCategory education)
        {
            EvaluationQuestionCategory = education;
        }

        public EvaluationQuestionCategory EvaluationQuestionCategory { get; set; }


    }

    public class AddEvaluationQuestionCategoryCommandHandler : IRequestHandler<AddEvaluationQuestionCategoryCommand>
    {
        private readonly IEvaluationQuestionCategoryRepository _educationRepository;

        public AddEvaluationQuestionCategoryCommandHandler(IEvaluationQuestionCategoryRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        public async Task Handle(AddEvaluationQuestionCategoryCommand request, CancellationToken cancellationToken)
        {

            await _educationRepository.AddAsync(request.EvaluationQuestionCategory);


        }
    }
}
