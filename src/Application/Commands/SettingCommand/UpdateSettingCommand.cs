using AWS.Dto.AwsDtos;
using AWS.Services;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Application.Commands.SettingCommand
{
    public sealed class UpdateSettingCommand : IRequest
    {
        public UpdateSettingCommand(Setting setting, IFormFile signatureLeft, IFormFile signatureRight)
        {
            Setting = setting;
            SignatureLeft = signatureLeft;
            SignatureRight = signatureRight;
        }

        public Setting Setting { get; set; }
        public IFormFile? SignatureLeft { get; set; }
        public IFormFile? SignatureRight { get; set; }

    }

    public class UpdateSettingCommandHandler : IRequestHandler<UpdateSettingCommand>
    {
        private readonly ISettingRepository _repository;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public UpdateSettingCommandHandler(ISettingRepository repository, IConfiguration config, IStorageService storageService)
        {
            _repository = repository;
            _config = config;
            _storageService = storageService;
        }

        public async Task Handle(UpdateSettingCommand request, CancellationToken cancellationToken)
        {
            if (request.SignatureRight != null)
            {
                try
                {
                    // Process file
                    await using var memoryStream = new MemoryStream();
                    await request.SignatureRight.CopyToAsync(memoryStream);

                    var fileExt = Path.GetExtension(request.SignatureRight.FileName);
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

                    var xresult = await _storageService.UploadFileReturnUrlAsync(s3Obj, cred, request.Setting.RightSignatureKey);
                    // 
                    if (xresult.Message.Contains("200"))
                    {
                        request.Setting.RightSignatureUrl = xresult.Url;
                        request.Setting.RightSignatureKey = xresult.Key;
                    }

                }
                catch (Exception c)
                {

                }
            }
            if (request.SignatureLeft != null)
            {
                try
                {
                    // Process file
                    await using var memoryStream = new MemoryStream();
                    await request.SignatureLeft.CopyToAsync(memoryStream);

                    var fileExt = Path.GetExtension(request.SignatureLeft.FileName);
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

                    var xresult = await _storageService.UploadFileReturnUrlAsync(s3Obj, cred, request.Setting.LeftSignatureKey);
                    // 
                    if (xresult.Message.Contains("200"))
                    {
                        request.Setting.LeftSignatureUrl = xresult.Url;
                        request.Setting.LeftSignatureKey = xresult.Key;
                    }

                }
                catch (Exception c)
                {

                }
            }

            await _repository.UpdateAsync(request.Setting);
        }
    }
}
