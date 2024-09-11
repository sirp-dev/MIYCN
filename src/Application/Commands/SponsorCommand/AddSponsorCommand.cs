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
      public sealed class AddSponsorCommand : IRequest
    {
        public AddSponsorCommand(Sponsor sponsor, IFormFile imageFile)
        {
            Sponsor = sponsor;
            ImageFile = imageFile;
        }

        public Sponsor Sponsor { get; set; }
        public IFormFile ImageFile { get; set; }

    }

    public class AddSponsorCommandHandler : IRequestHandler<AddSponsorCommand>
    {
        private readonly ISponsorRepository _sponsorRepository;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public AddSponsorCommandHandler(ISponsorRepository sponsorRepository, IConfiguration config, IStorageService storageService)
        {
            _sponsorRepository = sponsorRepository;
            _config = config;
            _storageService = storageService;
        }

        public async Task Handle(AddSponsorCommand request, CancellationToken cancellationToken)
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

            await _sponsorRepository.AddAsync(request.Sponsor);


        }
    }
}
