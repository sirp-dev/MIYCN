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
    public sealed class ListGalleryQuery : IRequest<List<Gallery>>
    {
        public class ListGalleryQueryHandler : IRequestHandler<ListGalleryQuery, List<Gallery>>
        {
            private readonly IGalleryRepository _repository;

            public ListGalleryQueryHandler(IGalleryRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Gallery>> Handle(ListGalleryQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }

}
