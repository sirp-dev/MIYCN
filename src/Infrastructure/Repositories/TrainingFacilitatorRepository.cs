using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public sealed class TrainingFacilitatorRepository : Repository<TrainingFacilitator>, ITrainingFacilitatorRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public TrainingFacilitatorRepository(AppDbContext context, UserManager<AppUser> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> AddFacilitator(TrainingFacilitator model)
        {
            var check = await _context.TrainingFacilitators.FirstOrDefaultAsync(x => x.TrainingId == model.TrainingId && x.UserId == model.UserId);
            if (check == null)
            {
                await AddAsync(model);
                return true;
            }
            return false;
        }

        public async Task<List<FacilitatorInTrainingDTo>> AllFacilitator()
        {
            var trainingFacilitators = await _context.TrainingFacilitators

               .Include(x => x.Training)
               .Include(x => x.User)
               .Where(tp => tp.User != null)
                               .Where(tp => tp.FacilitatorTrainingStatus == EnumStatus.FacilitatorTrainingStatus.Active)

               .ToListAsync();

            // Map to FacilitatorInTrainingDTO
            var participants = trainingFacilitators
                .Select(tp => new FacilitatorInTrainingDTo
                {
                    Id = tp.User.Id,
                    UniqueId = tp.User.UniqueId,
                    AccountToken = tp.User.AccountToken,
                    FirstName = tp.User.FirstName,
                    MiddleName = tp.User.MiddleName,
                    LastName = tp.User.LastName,
                    DateOfBirth = tp.User.DateOfBirth,
                    Date = tp.User.Date,
                    Religion = tp.User.Religion,
                    PhoneNumber = tp.User.PhoneNumber,
                    Email = tp.User.Email,
                    UserStatus = tp.User.UserStatus,
                    Gender = tp.User.Gender,
                    Role = tp.User.Role,
                    CurrentState = tp.User.CurrentState,
                    CurrentLga = tp.User.CurrentLga,
                    Address = tp.User.Address,
                    PlaceOfWork = tp.User.PlaceOfWork,
                    StateOrigin = tp.User.StateOrigin,
                    LgaOrigin = tp.User.LgaOrigin,
                    Country = tp.User.Country,
                    PassportFilePathUrl = tp.User.PassportFilePathUrl,
                    PassportFilePathKey = tp.User.PassportFilePathKey,
                    IdCardUrl = tp.User.IdCardUrl,
                    IdCardKey = tp.User.IdCardKey,
                    BankName = tp.User.BankName,
                    BankAccount = tp.User.BankAccount,
                    AccountNumber = tp.User.AccountNumber,
                    Logs = tp.User.Logs,
                    TrainingId = tp.TrainingId,
                    Title = tp.Training.Title,
                    State = tp.Training.State,
                    LGA = tp.Training.LGA,
                    Position = tp.Position
                })
                .ToList();

            return participants;
        }

        public async Task<FacilitatorInTrainingDTo> FacilitatorInTraining(long trainingId, string userId)
        {
            var participantDto = await _context.TrainingFacilitators
 
                .Where(tp => tp.TrainingId == trainingId && tp.UserId == userId)
                .Select(tp => new FacilitatorInTrainingDTo
                {
                    Id = tp.User.Id,
                    UniqueId = tp.User.UniqueId,
                    AccountToken = tp.User.AccountToken,
                    FirstName = tp.User.FirstName,
                    MiddleName = tp.User.MiddleName,
                    LastName = tp.User.LastName,
                    DateOfBirth = tp.User.DateOfBirth,
                    Date = tp.User.Date,
                    Religion = tp.User.Religion,
                    PhoneNumber = tp.User.PhoneNumber,
                    Email = tp.User.Email,
                    UserStatus = tp.User.UserStatus,
                    Gender = tp.User.Gender,
                    Role = tp.User.Role,
                    CurrentState = tp.User.CurrentState,
                    CurrentLga = tp.User.CurrentLga,
                    Address = tp.User.Address,
                    PlaceOfWork = tp.User.PlaceOfWork,
                    StateOrigin = tp.User.StateOrigin,
                    LgaOrigin = tp.User.LgaOrigin,
                    Country = tp.User.Country,
                    PassportFilePathUrl = tp.User.PassportFilePathUrl,
                    PassportFilePathKey = tp.User.PassportFilePathKey,
                    IdCardUrl = tp.User.IdCardUrl,
                    IdCardKey = tp.User.IdCardKey,
                    BankName = tp.User.BankName,
                    BankAccount = tp.User.BankAccount,
                    AccountNumber = tp.User.AccountNumber,
                    Logs = tp.User.Logs,
                    TrainingId = tp.TrainingId,
                    Title = tp.Training.Title,
                    State = tp.Training.State,
                    LGA = tp.Training.LGA,
                    Position = tp.Position,
                    FacilitatorTrainingId = tp.Id,
                    FacilitatorTrainingStatus = tp.FacilitatorTrainingStatus
                })
                .FirstOrDefaultAsync();

            return participantDto;
        }

        public async Task<List<FacilitatorInTrainingDTo>> FacilitatorInTraining(long trainingId)
        {
            //var participants = await _context.TrainingFacilitators
            //       .Where(tp => tp.TrainingId == trainingId)
            //       .Select(tp => new FacilitatorInTrainingDTo
            //       {
            //           Id = tp.User.Id,
            //           UniqueId = tp.User.UniqueId,
            //           AccountToken = tp.User.AccountToken,
            //           FirstName = tp.User.FirstName,
            //           MiddleName = tp.User.MiddleName,
            //           LastName = tp.User.LastName,
            //           DateOfBirth = tp.User.DateOfBirth,
            //           Date = tp.User.Date,
            //           Religion = tp.User.Religion,
            //           PhoneNumber = tp.User.PhoneNumber,
            //           Email = tp.User.Email,
            //           UserStatus = tp.User.UserStatus,
            //           Gender = tp.User.Gender,
            //           Role = tp.User.Role,
            //           CurrentState = tp.User.CurrentState,
            //           CurrentLga = tp.User.CurrentLga,
            //           Address = tp.User.Address,
            //           PlaceOfWork = tp.User.PlaceOfWork,
            //           StateOrigin = tp.User.StateOrigin,
            //           LgaOrigin = tp.User.LgaOrigin,
            //           Country = tp.User.Country,
            //           PassportFilePathUrl = tp.User.PassportFilePathUrl,
            //           PassportFilePathKey = tp.User.PassportFilePathKey,
            //           IdCardUrl = tp.User.IdCardUrl,
            //           IdCardKey = tp.User.IdCardKey,
            //           BankName = tp.User.BankName,
            //           BankAccount = tp.User.BankAccount,
            //           AccountNumber = tp.User.AccountNumber,
            //           Logs = tp.User.Logs,
            //           TrainingId = tp.TrainingId,
            //           Title = tp.Training.Title,
            //           State = tp.Training.State,
            //           LGA = tp.Training.LGA,
            //           Position = tp.Position
            //       })
            //       .ToListAsync();
            // Fetch the training facilitators
            var trainingFacilitators = await _context.TrainingFacilitators
 
                .Include(x => x.Training)
                .Include(x => x.User)
                .Where(tp => tp.TrainingId == trainingId && tp.User != null)
                .ToListAsync();

            // Map to FacilitatorInTrainingDTO
            var participants = trainingFacilitators
                .Select(tp => new FacilitatorInTrainingDTo
                {
                    Id = tp.User.Id,
                    UniqueId = tp.User.UniqueId,
                    AccountToken = tp.User.AccountToken,
                    FirstName = tp.User.FirstName,
                    MiddleName = tp.User.MiddleName,
                    LastName = tp.User.LastName,
                    DateOfBirth = tp.User.DateOfBirth,
                    Date = tp.User.Date,
                    Religion = tp.User.Religion,
                    PhoneNumber = tp.User.PhoneNumber,
                    Email = tp.User.Email,
                    UserStatus = tp.User.UserStatus,
                    Gender = tp.User.Gender,
                    Role = tp.User.Role,
                    CurrentState = tp.User.CurrentState,
                    CurrentLga = tp.User.CurrentLga,
                    Address = tp.User.Address,
                    PlaceOfWork = tp.User.PlaceOfWork,
                    StateOrigin = tp.User.StateOrigin,
                    LgaOrigin = tp.User.LgaOrigin,
                    Country = tp.User.Country,
                    PassportFilePathUrl = tp.User.PassportFilePathUrl,
                    PassportFilePathKey = tp.User.PassportFilePathKey,
                    IdCardUrl = tp.User.IdCardUrl,
                    IdCardKey = tp.User.IdCardKey,
                    BankName = tp.User.BankName,
                    BankAccount = tp.User.BankAccount,
                    AccountNumber = tp.User.AccountNumber,
                    Logs = tp.User.Logs,
                    TrainingId = tp.TrainingId,
                    Title = tp.Training.Title,
                    State = tp.Training.State,
                    LGA = tp.Training.LGA,
                    Position = tp.Position,
                    FacilitatorTrainingId = tp.Id,
                    FacilitatorTrainingStatus = tp.FacilitatorTrainingStatus
                })
                .ToList();

            return participants;
        }

        public async Task<TrainingFacilitator> GetFacilitatorById(long id)
        {
            var data = await _context.TrainingFacilitators
                .Include(x => x.Training)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public async Task UpdateFacilitatorInTrainingStatus(long trainingId, long facilitatorId, EnumStatus.FacilitatorTrainingStatus status, string reason)
        {
            var gettrainingupdate = await _context.TrainingFacilitators.FirstOrDefaultAsync(x => x.TrainingId == trainingId && x.Id == facilitatorId);
            if (gettrainingupdate != null)
            {
                gettrainingupdate.FacilitatorTrainingStatus = status;
                gettrainingupdate.Reasons = reason;
                _context.TrainingFacilitators.Update(gettrainingupdate);
                await _context.SaveChangesAsync();

                try
                {
                    var user = await _userManager.FindByIdAsync(gettrainingupdate.UserId);
                    if (user != null)
                    {
                        if (gettrainingupdate.FacilitatorTrainingStatus == EnumStatus.FacilitatorTrainingStatus.Disabled)
                        {
                            user.Role = user.Role.Replace("Facilitator", "");
                            await _userManager.UpdateAsync(user);
                            await _userManager.RemoveFromRoleAsync(user, "Facilitator");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(user.Role))
                            {
                                user.Role = "Facilitator";
                            }
                            else
                            {
                                user.Role = user.Role + ", Facilitator";
                            }
                            await _userManager.UpdateAsync(user);
                            await _userManager.AddToRoleAsync(user, "Facilitator");
                        }
                    }



                }
                catch (Exception ex)
                {
                }




            }
        }
    }
}
