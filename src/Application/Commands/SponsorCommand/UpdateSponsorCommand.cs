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

namespace Application.Commands.SponsorCommand
{
    public sealed class UpdateSponsorCommand : IRequest
    {

        public UpdateSponsorCommand(Sponsor sponsor, IFormFile imageFile)
        {
            Sponsor = sponsor;
            ImageFile = imageFile;
        }

        public Sponsor Sponsor { get; set; }
        public IFormFile ImageFile { get; set; }



    }

    public class UpdateSponsorCommandHandler : IRequestHandler<UpdateSponsorCommand>
    {
        private readonly ISponsorRepository _repository;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public UpdateSponsorCommandHandler(ISponsorRepository repository, IConfiguration config, IStorageService storageService)
        {
            _repository = repository;
            _config = config;
            _storageService = storageService;
        }

        public async Task Handle(UpdateSponsorCommand request, CancellationToken cancellationToken)
        {
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
                        request.Sponsor.LogoUrl = xresult.Url;
                        request.Sponsor.LogoKey = xresult.Key;
                    }

                }
                catch (Exception c)
                {

                }
            }

            await _repository.UpdateAsync(request.Sponsor);
        }
    }
}
