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
using static Domain.Models.EnumStatus;

namespace Infrastructure.Repositories
{
    public sealed class TrainingParticipantRepository : Repository<TrainingParticipant>, ITrainingParticipantRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserTestRepository _userTestRepository;
        public TrainingParticipantRepository(AppDbContext context, UserManager<AppUser> userManager, IUserTestRepository userTestRepository) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _userTestRepository = userTestRepository;
        }

        public async Task<bool> AddParticipant(TrainingParticipant model)
        {
            var check = await _context.TrainingParticipants.FirstOrDefaultAsync(x => x.TrainingId == model.TrainingId && x.UserId == model.UserId);
            if (check == null)
            {
                await AddAsync(model);
                return true;
            }
            return false;
        }

        public async Task<List<ParticipantInTrainingDTo>> AllParticipants()
        {
            var participants = await _context.TrainingParticipants
                .Where(tp => tp.ParticipantTrainingStatus == EnumStatus.ParticipantTrainingStatus.Active)
                .Select(tp => new ParticipantInTrainingDTo
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
                    ParticipantTrainingStatus = tp.ParticipantTrainingStatus
                })
                .ToListAsync();

            return participants;
        }

        public Task<bool> CheckParticipantInTraining(long trainingId, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<TrainingParticipant> GetParticipantById(long id)
        {
            var data = await _context.TrainingParticipants
                .Include(x => x.Training)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }






        public async Task<ParticipantInTrainingDTo> ParticipantInTraining(long trainingId, string userId)
        {
            var participantDto = await _context.TrainingParticipants
                .Where(tp => tp.TrainingId == trainingId && tp.UserId == userId && tp.ParticipantTrainingStatus == EnumStatus.ParticipantTrainingStatus.Active)
                .Select(tp => new ParticipantInTrainingDTo
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
                    LGA = tp.Training.LGA
                })
                .FirstOrDefaultAsync();

            return participantDto;
        }

        public async Task<List<ParticipantInTrainingDTo>> ParticipantInTraining(long trainingId)
        {
            var participants = await _context.TrainingParticipants
                   .Where(tp => tp.TrainingId == trainingId)
                   .Select(tp => new ParticipantInTrainingDTo
                   {
                       ParticipantTrainingId = tp.Id,
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
                       ParticipantTrainingStatus = tp.ParticipantTrainingStatus,
                       SmsSent = tp.User.SmsSent
                   })
                   .ToListAsync();

            return participants;
        }

        public async Task UpdateParticipantInTrainingStatus(long trainingId, long participantId, ParticipantTrainingStatus status, string reason)
        {
            var gettrainingupdate = await _context.TrainingParticipants.FirstOrDefaultAsync(x => x.TrainingId == trainingId && x.Id == participantId);
            if (gettrainingupdate != null)
            {
                gettrainingupdate.ParticipantTrainingStatus = status;
                gettrainingupdate.Reasons = reason;
                _context.TrainingParticipants.Update(gettrainingupdate);
                await _context.SaveChangesAsync();

                try
                {
                    var user = await _userManager.FindByIdAsync(gettrainingupdate.UserId);
                    if (user != null)
                    {
                         
                        if (gettrainingupdate.ParticipantTrainingStatus == EnumStatus.ParticipantTrainingStatus.Disabled)
                        {
                            user.Role = user.Role.Replace("Participant", "");
                            await _userManager.UpdateAsync(user);
                            await _userManager.RemoveFromRoleAsync(user, "Participant");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(user.Role))
                            {
                                user.Role = "Participant";
                            }
                            else
                            {
                                user.Role = user.Role + ", Participant";
                            }
                            await _userManager.UpdateAsync(user);
                            await _userManager.AddToRoleAsync(user, "Participant");
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
