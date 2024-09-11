using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TrainingQueries
{
    //public sealed class ListTrainingWithDetailsQuery : IRequest<List<Training>>
    //{
         
    //    public class ListTrainingQueryHandler : IRequestHandler<ListTrainingQuery, List<Training>>
    //    {
    //        private readonly ITrainingRepository _repository;

    //        public ListTrainingQueryHandler(ITrainingRepository repository)
    //        {
    //            _repository = repository;
    //        }

    //        public async Task<List<Training>> Handle(ListTrainingQuery request, CancellationToken cancellationToken)
    //        {
    //            return await _repository.GetAllTrainingWithDetails();

    //        }
    //    }
    //}

}
