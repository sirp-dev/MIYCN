using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.IdentityQueries
{
      public class GetUserByNameQuery : IRequest<BasicProfileDto>
    {
        public GetUserByNameQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, BasicProfileDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;


        public GetUserByNameQueryHandler(
            UserManager<AppUser> userManager
, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<BasicProfileDto> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new ArgumentNullException(nameof(request.Id));
            }

            var UserDatas = await _userManager.FindByNameAsync(request.Id);

            if (UserDatas == null)
            {
                throw new ArgumentException("User not found");
            }


            var infoModel = new BasicProfileDto
            {
                Id = UserDatas.Id,
                FullnameX = UserDatas.FullnameX,
                PhoneNumber = UserDatas.PhoneNumber,
                Email = UserDatas.Email,
                Date = UserDatas.Date,
                UserStatus = UserDatas.UserStatus,
                Gender = UserDatas.Gender,
                Role = UserDatas.Role,
                ResetPassword = UserDatas.ResetPassword,
                TempPass = UserDatas.TempPass,
                SmsSent = UserDatas.SmsSent,
                EmailSent = UserDatas.EmailSent,
            };


            return infoModel;
        }
    }
}
