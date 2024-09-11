using Application.Validators;
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
        public sealed class GetByIdGalleryQuery : IRequest<Gallery>
    {
        public GetByIdGalleryQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdGalleryQueryHandler : IRequestHandler<GetByIdGalleryQuery, Gallery>
        {

            private readonly IGalleryRepository _repository;

            public GetByIdGalleryQueryHandler(IGalleryRepository repository)
            {
                _repository = repository;
            }
            public async Task<Gallery> Handle(GetByIdGalleryQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                Gallery data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
