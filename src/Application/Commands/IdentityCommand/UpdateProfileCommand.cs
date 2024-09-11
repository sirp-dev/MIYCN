using Application.Commands.DTO;
using Application.Commands.EmailCommand;
using Application.Queries.IdentityQueries;
using AWS.Dto.AwsDtos;
using AWS.Services;
using Domain.Models;
using Infrastructure.Migrations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PostmarkEmailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZSMS.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Commands.IdentityCommand
{

    public class UpdateProfileCommand : IRequest<AppResponse>
    {
        public UpdateProfileCommand(string userId, FullProfileDto input, IFormFile imageFile, IFormFile imageIdCard)
        {
            UserId = userId;
            Input = input;
            ImageFile = imageFile;
            ImageIdCard = imageIdCard;
        }

        public string UserId { get; set; }
        public FullProfileDto Input { get; set; }
        public IFormFile ImageFile { get; set; }
        public IFormFile ImageIdCard { get; set; }
    }

    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, AppResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        private readonly IMediator _mediator;

        public UpdateProfileCommandHandler(UserManager<AppUser> userManager, IConfiguration config, IStorageService storageService, IMediator mediator)
        {
            _userManager = userManager;
            _config = config;
            _storageService = storageService;
            _mediator = mediator;
        }

        public async Task<AppResponse> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                var userinfo = await _userManager.FindByIdAsync(request.UserId);

                //  code for handling image upload:
                if (request.ImageFile != null)
                {
                    try
                    {
                        // Process file
                        await using var memoryStream = new MemoryStream();
                        await request.ImageFile.CopyToAsync(memoryStream);

                        var fileExt = Path.GetExtension(request.ImageFile.FileName);
                        var docName = $"{Guid.NewGuid()}{fileExt}";
                        // call server

                        var s3Obj = new S3Object()
                        {
                            BucketName = await _storageService.BucketName(),
                            InputStream = memoryStream,
                            Name = docName
                        };

                        var cred = new AwsCredentials()
                        {
                            AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                            SecretKey = _config["AwsConfiguration:AWSSecretKey"]
                        };

                        var xresult = await _storageService.UploadFileReturnUrlAsync(s3Obj, cred, "");
                        // 




                        if (xresult.Message.Contains("200"))
                        {
                            userinfo.PassportFilePathUrl = xresult.Url;
                            userinfo.PassportFilePathKey = xresult.Key;
                        }

                    }
                    catch (Exception c)
                    {

                    }
                }
                if (request.ImageIdCard != null)
                {
                    try
                    {
                        // Process file
                        await using var memoryStream = new MemoryStream();
                        await request.ImageIdCard.CopyToAsync(memoryStream);

                        var fileExt = Path.GetExtension(request.ImageIdCard.FileName);
                        var docName = $"{Guid.NewGuid()}{fileExt}";
                        // call server

                        var s3Obj = new S3Object()
                        {
                            BucketName = await _storageService.BucketName(),
                            InputStream = memoryStream,
                            Name = docName
                        };

                        var cred = new AwsCredentials()
                        {
                            AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                            SecretKey = _config["AwsConfiguration:AWSSecretKey"]
                        };

                        var xresult = await _storageService.UploadFileReturnUrlAsync(s3Obj, cred, request.Input.IdCardKey);
                        //

                        if (xresult.Message.Contains("200"))
                        {
                            userinfo.IdCardUrl = xresult.Url;
                            userinfo.IdCardKey = xresult.Key;
                        }

                    }
                    catch (Exception c)
                    {

                    }
                }
                // Your remaining logic for updating profile...
                userinfo.FirstName = request.Input.FirstName;
                userinfo.LastName = request.Input.LastName;
                userinfo.MiddleName = request.Input.MiddleName;

                userinfo.Religion = request.Input.Religion;
                userinfo.UserStatus = request.Input.UserStatus;

                userinfo.Gender = request.Input.Gender;
                userinfo.CurrentState = request.Input.CurrentState;
                userinfo.CurrentLga = request.Input.CurrentLga;


                userinfo.Address = request.Input.Address;
                userinfo.PlaceOfWork = request.Input.PlaceOfWork;
                userinfo.StateOrigin = request.Input.StateOrigin;
                if (request.Input.LgaOrigin != null)
                {
                    userinfo.LgaOrigin = request.Input.LgaOrigin;
                }
                userinfo.Country = request.Input.Country;
                userinfo.BankName = request.Input.BankName;

                if (request.Input.DateOfBirth != null)
                {
                    userinfo.DateOfBirth = request.Input.DateOfBirth;
                }
                userinfo.Gender = request.Input.Gender;
                userinfo.MaritalStatus = request.Input.MaritalStatus;
                userinfo.BankName = request.Input.BankName;


                userinfo.BankAccount = request.Input.BankAccount;
                userinfo.AccountNumber = request.Input.AccountNumber;
                userinfo.UpdateProfile = request.Input.UpdateProfile;
                userinfo.UpdateEducation = request.Input.UpdateEducation;
                userinfo.UpdateExperience = request.Input.UpdateExperience;


                userinfo.FacilitatorRole = request.Input.FacilitatorRole;
                userinfo.DescribeFacilitatorRole = request.Input.DescribeFacilitatorRole;

                // Save changes to database
                await _userManager.UpdateAsync(userinfo);

                return new AppResponse { Success = true, Message = "Profile updated successfully." };
            }
            catch (Exception ex)
            {

                return new AppResponse { Success = false, Message = "Failed to update profile." };
            }

        }

    }
}