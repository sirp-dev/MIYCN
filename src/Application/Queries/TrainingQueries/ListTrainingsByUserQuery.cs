using Domain.DTOs;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TrainingQueries
{
    public sealed class ListTrainingsByUserQuery : IRequest<List<TrainingByUserDto>>
    {
        public string UserId { get; }

        public ListTrainingsByUserQuery(string userId)
        {
            UserId = userId;
        }
    }

    public class ListTrainingsByUserQueryHandler : IRequestHandler<ListTrainingsByUserQuery, List<TrainingByUserDto>>
    {
        private readonly ITrainingRepository _repository;

        public ListTrainingsByUserQueryHandler(ITrainingRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TrainingByUserDto>> Handle(ListTrainingsByUserQuery request, CancellationToken cancellationToken)
        {
            var trainings = await _repository.GetAllTrainingsByUserId(request.UserId);

            
            return trainings;
        }
    }

}
