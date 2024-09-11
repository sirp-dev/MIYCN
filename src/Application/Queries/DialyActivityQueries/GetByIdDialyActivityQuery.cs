using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.DialyActivityQueries
{
         public sealed class GetByIdDialyActivityQuery : IRequest<DialyActivity>
    {
        public GetByIdDialyActivityQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdDialyActivityQueryHandler : IRequestHandler<GetByIdDialyActivityQuery, DialyActivity>
        {

            private readonly IDialyActivityRepository _repository;

            public GetByIdDialyActivityQueryHandler(IDialyActivityRepository repository)
            {
                _repository = repository;
            }
            public async Task<DialyActivity> Handle(GetByIdDialyActivityQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                DialyActivity data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
