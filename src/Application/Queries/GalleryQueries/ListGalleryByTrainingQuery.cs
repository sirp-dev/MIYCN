using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GalleryQueries
{ 
    public sealed class ListGalleryByTrainingQuery : IRequest<List<Gallery>>
    {
        public long TrainingId { get; set; }

        public ListGalleryByTrainingQuery(long trainingId)
        {
            TrainingId = trainingId;
        }

        public class ListGalleryByTrainingQueryHandler : IRequestHandler<ListGalleryByTrainingQuery, List<Gallery>>
        {
            private readonly IGalleryRepository _repository;

            public ListGalleryByTrainingQueryHandler(IGalleryRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Gallery>> Handle(ListGalleryByTrainingQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAll(request.TrainingId);

            }
        }
    }
}
