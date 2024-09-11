using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace Application.Commands.IdentityCommand
{
    public sealed class UploadParticipantsCommand : IRequest<bool>
    {
        public UploadParticipantsCommand(IFormFile excelFile, long trainingId)
        {
            ExcelFile = excelFile;
            TrainingId = trainingId;
        }

        public IFormFile ExcelFile { get; set; }
        public long TrainingId { get; set; }

    }

    public class UploadParticipantsCommandHandler : IRequestHandler<UploadParticipantsCommand, bool>
    {
        private readonly IMediator _mediator;

        public UploadParticipantsCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(UploadParticipantsCommand request, CancellationToken cancellationToken)
        {
            List<UpdateDto> updateDto = new List<UpdateDto>();
            List<RegisterDto> registerDto = new List<RegisterDto>();
            string Role = "";
            // Set the license context before using EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var stream = request.ExcelFile.OpenReadStream())
            {
                using (var package = new ExcelPackage(stream))
                {
                    // Assuming the first sheet contains the data
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                    if (worksheet != null)
                    {
                        int rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++) // Start from 2 to skip header row
                        {
                            var emailList = new UpdateDto
                            {
                                Name = worksheet.Cells[row, 2].Value?.ToString(),
                                Office = worksheet.Cells[row, 3].Value?.ToString(),
                                Phone = worksheet.Cells[row, 4].Value?.ToString(),
                                Email = worksheet.Cells[row, 5].Value?.ToString(),
                                Role = worksheet.Cells[row, 6].Value?.ToString(),

                            };
                            RegisterDto fillregdto = new RegisterDto();
                            if (emailList.Email != null)
                            {
                                if (emailList.Phone != null)
                                {
                                    // Remove punctuation marks using Regex
                                    if (emailList.Name != null)
                                    {
                                        if (emailList.Role != null)
                                        {
                                            if(emailList.Role == "Participant" || emailList.Role == "Facilitator") { 
                                            string cleanFullName = Regex.Replace(emailList.Name, @"[^\w\s]", "");

                                            // Split the cleaned full name into words
                                            string[] nameParts = cleanFullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                            // Assign the first three names to the model
                                            fillregdto.FirstName = nameParts.Length > 0 ? nameParts[0] : string.Empty;
                                            fillregdto.MiddleName = nameParts.Length > 1 ? nameParts[1] : string.Empty;
                                            fillregdto.LastName = nameParts.Length > 2 ? nameParts[2] : string.Empty;
                                            fillregdto.PlaceOfWork = emailList.Office;
                                            fillregdto.Email = emailList.Email.Replace(" ", "");
                                            fillregdto.Phone = emailList.Phone.Replace(" ", string.Empty);
                                            fillregdto.Position = emailList.Role;

                                            registerDto.Add(fillregdto);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            try
            {
                foreach (var item in registerDto)
                {
                    AddUserByTrainingIdCommand regCommand = new AddUserByTrainingIdCommand(item, item.Position, request.TrainingId, null);
                    var response = await _mediator.Send(regCommand);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }


        }
    }

}
