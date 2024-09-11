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

namespace Application.Commands.GalleryCommand
{
      public sealed class AddGalleryCommand : IRequest
    {
        public AddGalleryCommand(Gallery sponsor, IFormFile imageFile)
        {
            Gallery = sponsor;
            ImageFile = imageFile;
        }

        public Gallery Gallery { get; set; }
        public IFormFile ImageFile { get; set; }

    }

    public class AddGalleryCommandHandler : IRequestHandler<AddGalleryCommand>
    {
        private readonly IGalleryRepository _sponsorRepository;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public AddGalleryCommandHandler(IGalleryRepository sponsorRepository, IConfiguration config, IStorageService storageService)
        {
            _sponsorRepository = sponsorRepository;
            _config = config;
            _storageService = storageService;
        }

        public async Task Handle(AddGalleryCommand request, CancellationToken cancellationToken)
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
                        request.Gallery.ImageUrl = xresult.Url;
                        request.Gallery.ImageKey = xresult.Key;
                    }

                }
                catch (Exception c)
                {

                }
            }

            await _sponsorRepository.AddAsync(request.Gallery);


        }
    }
}
