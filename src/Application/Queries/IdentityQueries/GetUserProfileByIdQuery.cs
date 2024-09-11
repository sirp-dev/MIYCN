using Application.Queries.ProfileCategoryQueries;
using Application.Services;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Queries.IdentityQueries
{
    public class GetUserProfileByIdQuery : IRequest<FullProfileDto>
    {
        public GetUserProfileByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery, FullProfileDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;


        public GetUserProfileByIdQueryHandler(
            UserManager<AppUser> userManager
, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<FullProfileDto> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new ArgumentNullException(nameof(request.Id));
            }

            var UserDatas = await _userManager.FindByIdAsync(request.Id);

            if (UserDatas == null)
            {
                throw new ArgumentException("User not found");
            }
            var infoModelCommand = new GetByUserIdProfileCategoryQuery(request.Id);
            var ProfileCategories = await _mediator.Send(infoModelCommand);
 

            var infoModel = new FullProfileDto
            {
                Id = UserDatas.Id,
                FullnameX = UserDatas.FullnameX,
                PhoneNumber = UserDatas.PhoneNumber,
                Email = UserDatas.Email,
                UniqueId = UserDatas.UniqueId,
                AccountToken = UserDatas.AccountToken,
                FirstName = UserDatas.FirstName,
                MiddleName = UserDatas.MiddleName,
                LastName = UserDatas.LastName,
                DateOfBirth = UserDatas.DateOfBirth,
                Date = UserDatas.Date,
                Religion = UserDatas.Religion,
                UserStatus = UserDatas.UserStatus,
                Gender = UserDatas.Gender,
                Role = UserDatas.Role,
                CurrentState = UserDatas.CurrentState,
                CurrentLga = UserDatas.CurrentLga,
                Address = UserDatas.Address,
                PlaceOfWork = UserDatas.PlaceOfWork,
                StateOrigin = UserDatas.StateOrigin,
                LgaOrigin = UserDatas.LgaOrigin,
                Country = UserDatas.Country,
                PassportFilePathUrl = UserDatas.PassportFilePathUrl,
                PassportFilePathKey = UserDatas.PassportFilePathKey,
                IdCardUrl = UserDatas.IdCardUrl,
                IdCardKey = UserDatas.IdCardKey,
                BankName = UserDatas.BankName,
                BankAccount = UserDatas.BankAccount,
                AccountNumber = UserDatas.AccountNumber,
                Logs = UserDatas.Logs,
                LastLogin = UserDatas.LastLogin,
                ProfileCategories = ProfileCategories
            };

            Zen.Barcode.CodeQrBarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            string userinfo = "";
            try
            {
                infoModel.ProfileLink = "https://miycnportal.com/uap/" + UserDatas.UniqueId;
                System.Drawing.Image img = barcode.Draw(infoModel.ProfileLink, 100);

                infoModel.BarcodeImage = AppServices.TurnImageToByteArray(img);
            }
            catch (Exception c)
            {

            }

            return infoModel;
        }
    }
}
