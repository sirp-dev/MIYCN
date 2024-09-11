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

namespace Application.Commands.TrainingCommand
{
    public sealed class UpdateTrainingCommand : IRequest
    {
        public UpdateTrainingCommand(Training data, IFormFile? leftsignaturefile, IFormFile? rightsignaturefile)
        {
            Training = data;
            Leftsignaturefile = leftsignaturefile;
            Rightsignaturefile = rightsignaturefile;
        }

        public Training Training { get; set; }
        public IFormFile? Leftsignaturefile { get; set; }
        public IFormFile? Rightsignaturefile { get; set; }

    }

    public class UpdateTrainingCommandHandler : IRequestHandler<UpdateTrainingCommand>
    {
        private readonly ITrainingRepository _repository;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public UpdateTrainingCommandHandler(ITrainingRepository repository, IConfiguration config, IStorageService storageService)
        {
            _repository = repository;
            _config = config;
            _storageService = storageService;
        }

        public async Task Handle(UpdateTrainingCommand request, CancellationToken cancellationToken)
        {
            //imgae
            if (request.Leftsignaturefile != null)
            {
                try
                {
                    // Process file
                    await using var memoryStream = new MemoryStream();
                    await request.Leftsignaturefile.CopyToAsync(memoryStream);

                    var fileExt = Path.GetExtension(request.Leftsignaturefile.FileName);
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
                        request.Training.CertificateLeftSideSignatureUrl = xresult.Url;
                        request.Training.CertificateLeftSideSignatureKey = xresult.Key;
                    }

                }
                catch (Exception c)
                {

                }
            }

            //imgae
            if (request.Rightsignaturefile != null)
            {
                try
                {
                    // Process file
                    await using var memoryStream = new MemoryStream();
                    await request.Rightsignaturefile.CopyToAsync(memoryStream);

                    var fileExt = Path.GetExtension(request.Rightsignaturefile.FileName);
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
                        request.Training.CertificateRightSideSignatureUrl = xresult.Url;
                        request.Training.CertificateRightSideSignatureKey = xresult.Key;
                    }

                }
                catch (Exception c)
                {

                }
            }


            await _repository.UpdateAsync(request.Training);
        }
    }
}
