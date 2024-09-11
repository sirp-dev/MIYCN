using Azure.Core;
using BitMiracle.LibTiff.Classic;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using XYZSMS.DTO;
using Application.Services;
using Application.Commands.ProfileCategoryCommand;
using Application.Commands.EmailCommand;
using PostmarkEmailService;
using Application.Commands.SmsCommand;

namespace Application.Commands.IdentityCommand
{


    public sealed class AddUserCommand : IRequest<RegisterResponseDto>
    {
        public AddUserCommand(RegisterDto registerDto, string role)
        {
            RegisterDto = registerDto;
            Role = role;
        }

        public RegisterDto RegisterDto { get; set; }
        public string Role { get; set; }
    }


    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, RegisterResponseDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;
        private readonly RoleManager<AppRole> _role;


        public AddUserCommandHandler(
            UserManager<AppUser> userManager, IMediator mediator, RoleManager<AppRole> role)
        {
            _userManager = userManager;
            _mediator = mediator;
            _role = role;
        }


        public async Task<RegisterResponseDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            RegisterResponseDto regResponse = new RegisterResponseDto();
            var checkUserExist = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.RegisterDto.Email);

            if (checkUserExist != null)
            {
                regResponse.UserId = checkUserExist.Id;
                regResponse.Role = checkUserExist.Role;
                if(regResponse.Role != null) { 
                regResponse.Success = true;
                }
                return regResponse;
            }
            
            try
            {
                var user = new AppUser
                {
                    UserName = request.RegisterDto.Email.Replace(" ", ""),
                    Email = request.RegisterDto.Email.Replace(" ", ""),
                    PhoneNumber = request.RegisterDto.Phone,
                    FirstName = request.RegisterDto.FirstName,
                    LastName = request.RegisterDto.LastName,
                    MiddleName = request.RegisterDto.MiddleName,
                    Date = DateTime.UtcNow.AddHours(1),
                    Role = request.Role,
                    ResetPassword = true,
                    TempPass = AppServices.GenerateRandomAlphaNumeric(8),
                    UpdateEducation = true,
                    UpdateExperience = true,
                    LockoutEnabled = false,
                    UpdateProfile = true,
                    UserStatus = EnumStatus.UserStatus.Active,
                    PlaceOfWork = request.RegisterDto.PlaceOfWork,
                };

                while (user.UniqueId == null)
                {
                    string uniqId = AppServices.GenerateRandomAlphaNumericUniqueId(8);

                    var check = await _userManager.Users.FirstOrDefaultAsync(x => x.UniqueId == uniqId);

                    if (check == null)
                    {
                        user.UniqueId = uniqId;
                        break;
                    }
                }

                user.Id = Guid.NewGuid().ToString();

                var result = await _userManager.CreateAsync(user, user.TempPass);

                if (result.Succeeded)
                {
                    regResponse.UserId = user.Id;
                    
                    

                    //
                    AppRole Managerf = new AppRole
                    {
                        Name = request.Role,
                        Description = "Description for the new role" // You can set this to any value you want
                    };
                    //var checkManagerf = await _role.FindByNameAsync(request.Role);

                    //if (checkManagerf == null)
                    //{
                    //   // await _role.CreateAsync(Managerf);
                    //}

                    var addrole = await _userManager.AddToRoleAsync(user, request.Role);
                    if (addrole.Succeeded)
                    {
                        regResponse.AddedToRole = true;
                        regResponse.Role = request.Role;
                    }
                    try
                    {
                        List<string> category = new List<string> { "EDUCATION", "EXPERIENCE" };

                        foreach (string item in category)
                        {
                            ProfileCategory p = new ProfileCategory();
                            p.AppUserId = user.Id;
                            p.Title = item;
                            AddProfileCategoryCommand profilecatcmd = new AddProfileCategoryCommand(p);
                            await _mediator.Send(profilecatcmd);
                        }
                    }
                    catch (Exception c)
                    {
                    }
                    var getUserForUpdate = await _userManager.FindByIdAsync(user.Id);
                    //try
                    //{
                    //    string baseUrl = "https://miycnportal.ng/"; // replace with your application base URL
                    //    string relativeUrl = "/Identity/Account/Login";
                    //    Uri baseUri = new Uri(baseUrl);
                    //    Uri loginUri = new Uri(baseUri, relativeUrl);

                    //    // Now you can use the loginUri to create the link
                    //    string linkcomplete = loginUri.AbsoluteUri;

                    //    string datavalue = $"<a href='{HtmlEncoder.Default.Encode(linkcomplete)}'>click here to login</a>.";

                    //    string messagedetails = $"Login Email: {user.Email} <br>Temporal Password: {user.TempPass}<br>" +
                    //        $"" + datavalue + "or copy the link before and paste in your browser to continue<br><br>" +
                    //        linkcomplete + "<br>" +
                    //        "<h4>INSTRUCTION</h4>" +

                    //        $"<h6>Kindly login, reset your password and update your profile.</h6>";

                    //    //send email
                    //    MessageDto msn = new MessageDto();
                    //    msn.Email = user.Email;
                    //    msn.Message = messagedetails;
                    //    msn.Subject = "MIYCN REGISTRATION";
                    //    msn.Name = user.FirstName + " " + user.LastName;
                    //    SendMessageCommand emailcommand = new SendMessageCommand(msn);
                    //    PostmarkResponse responseemail = await _mediator.Send(emailcommand);
                    //    if (responseemail.Status == PostmarkStatus.Success)
                    //    {

                    //        regResponse.EmailSent = true;
                    //        getUserForUpdate.EmailSent = true;

                    //    }

                    //}
                    //catch (Exception e)
                    //{

                    //}
                    //try
                    //{
                    //    SendSmsCommand smscommand = new SendSmsCommand(user.PhoneNumber, "Kindly check your email to complete your registration.");
                    //    var smsresponse = await _mediator.Send(smscommand);
                    //    regResponse.SmsSent = true;
                    //    getUserForUpdate.SmsSent = true;
                    //}
                    //catch (Exception c) { }

                    await _userManager.UpdateAsync(user);
                    regResponse.Message = "Registration Successfull";
                   
                    return regResponse;
                }
            }
            catch (Exception e)
            {
                regResponse.Message = e.ToString();
            }
            return regResponse;
        }
    }
}