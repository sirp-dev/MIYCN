using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public sealed class TrainingRepository : Repository<Training>, ITrainingRepository
    {
        private readonly AppDbContext _context;

        public TrainingRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IQueryable<Training>> GetAllTrainingWithDetails()
        {
            var trainings = _context.Trainings
                .Include(x => x.TrainingFacilitators).ThenInclude(x => x.User)
                .Include(x => x.TrainingParticipants).ThenInclude(x => x.User).OrderBy(x => x.State)
                .AsQueryable();


            return trainings;
        }
        public async Task<List<Training>> GetAllByCategoryId(string? state, long id = 0)
        {
            var trainings = await _context.Trainings
                .Include(x => x.TrainingCategory)

                .ToListAsync();
            if (id > 0)
            {
                trainings = trainings.Where(x => x.TrainingCategoryId == id).ToList();
            }
            if (state == "all")
            {

            }
            else
            {
                if (state != null)
                {
                    trainings = trainings.Where(x => x.State.ToLower() == state.ToLower()).ToList();
                }
                else
                {
                    trainings = trainings.Where(x => x.State.ToLower() == "").ToList();
                }
            }
            return trainings;
        }

        public async Task<List<Training>> GetAll(string? state)
        {
            var trainings = await _context.Trainings
                .ToListAsync();

            if (state == "all")
            {

            }
            else
            {
                if (state != null)
                {
                    trainings = trainings.Where(x => x.State.ToLower() == state.ToLower()).ToList();
                }
                else
                {
                    trainings = trainings.Where(x => x.State.ToLower() == "").ToList();
                }
            }
            return trainings;
        }

        public async Task<List<TrainingByUserDto>> GetAllTrainingsByUserId(string userId)
        {
            var trainings = await _context.Trainings
                .Include(t => t.TrainingFacilitators)
                    .ThenInclude(tf => tf.User)
                .Include(t => t.TrainingParticipants)
                    .ThenInclude(tp => tp.User)
                .Where(t => t.TrainingFacilitators.Any(tf => tf.UserId == userId) ||
                            t.TrainingParticipants.Any(tp => tp.UserId == userId))
                .ToListAsync();

            var trainingDtos = new List<TrainingByUserDto>();

            foreach (var training in trainings)
            {
                foreach (var facilitator in training.TrainingFacilitators.Where(tf => tf.UserId == userId))
                {
                    trainingDtos.Add(new TrainingByUserDto
                    {
                        UserId = userId,
                        TrainingId = training.Id,
                        TrainingAddress = training.Address,
                        Type = "Facilitator",
                        FacilitatorPosition = facilitator.Position,
                        TrainingTitle = training.Title,
                        TrainingDate = training.StartDate, // or any date property you prefer
                        UserEmail = facilitator.User.Email,
                        UserPhone = facilitator.User.PhoneNumber,
                        UserState = facilitator.User.CurrentState,
                        UserLGA = facilitator.User.CurrentLga,
                        PlaceOfWork = facilitator.User.PlaceOfWork,
                        TrainingLGA = training.LGA,
                        TrainingState = training.State,
                        TrainingStatus = training.TrainingStatus,
                        SignInStartTime = training.SignInStartTime,
                        SignInStopTime = training.SignInStopTime,
                        SignOutStartTime = training.SignOutStartTime,
                        SignOutStopTime = training.SignOutStopTime,
                    });
                }

                foreach (var participant in training.TrainingParticipants.Where(tp => tp.UserId == userId))
                {
                    trainingDtos.Add(new TrainingByUserDto
                    {
                        UserId = userId,
                        TrainingId = training.Id,
                        TrainingAddress = training.Address,
                        Type = "Participant",
                        TrainingTitle = training.Title,
                        TrainingDate = training.StartDate, // or any date property you prefer
                        UserEmail = participant.User.Email,
                        UserPhone = participant.User.PhoneNumber,
                        UserState = participant.User.CurrentState,
                        UserLGA = participant.User.CurrentLga,
                        PlaceOfWork = participant.User.PlaceOfWork,
                        SignInStartTime = training.SignInStartTime,
                        SignInStopTime = training.SignInStopTime,
                        SignOutStartTime = training.SignOutStartTime,
                        SignOutStopTime = training.SignOutStopTime,
                        TrainingLGA = training.LGA,
                        TrainingState = training.State,
                        TrainingStatus = training.TrainingStatus
                    });
                }
            }

            return trainingDtos;
        }


        public async Task<TrainingDto> GetTrainingByIdAndCounts(long id)
        {
            var trainingDto = await _context.Trainings
        .Where(x => x.Id == id)
        .Select(x => new TrainingDto
        {
            Id = x.Id,
            Title = x.Title,
            Address = x.Address,
            State = x.State,
            LGA = x.LGA,
            Ward = x.Ward,
            EnablePostTest = x.EnablePostTest,
            EnablePreTest = x.EnablePreTest,
            PostTestStartTime = x.PostTestStartTime,
            StartDate = x.StartDate,
            PreTestInstruction = x.PreTestInstruction,
            PostTestInstruction = x.PostTestInstruction,
            EndDate = x.EndDate,
            TrainingStatus = x.TrainingStatus,
            DialyStartTime = x.DialyStartTime,
            DialyEndTime = x.DialyEndTime,
            SignInStartTime = x.SignInStartTime,
            SignInStopTime = x.SignInStopTime,
            SignOutStartTime = x.SignOutStartTime,
            SignOutStopTime = x.SignOutStopTime,
            Sponsors = x.Sponsors.Count(),
            TrainingFacilitators = x.TrainingFacilitators.Count(),
            TrainingParticipants = x.TrainingParticipants.Where(x => x.ParticipantTrainingStatus == EnumStatus.ParticipantTrainingStatus.Active).Count(),

            DialyActivities = x.DialyActivities.Count(),
            TestCategory = x.TestCategory.Count(),

            CertificateUseRightSidePhysicalSignature = x.CertificateUseRightSidePhysicalSignature,
            CertificateRightSideSignatureUrl = x.CertificateRightSideSignatureUrl,
            CertificateRightSideSignatureKey = x.CertificateRightSideSignatureKey,
            CertificateRightSideName = x.CertificateRightSideName,
            CertificateRightSideOfficePosition = x.CertificateRightSideOfficePosition,
            CertificateRightSideOfficeTitle = x.CertificateRightSideOfficeTitle,
            CertificateUseLeftSidePhysicalSignature = x.CertificateUseLeftSidePhysicalSignature,
            CertificateLeftSideSignatureUrl = x.CertificateLeftSideSignatureUrl,
            CertificateLeftSideSignatureKey = x.CertificateLeftSideSignatureKey,
            CertificateLeftSideName = x.CertificateLeftSideName,
            CertificateLeftSideOfficePosition = x.CertificateLeftSideOfficePosition,
            CertificateLeftSideOfficeTitle = x.CertificateLeftSideOfficeTitle,
            CertificateTitle = x.CertificateTitle,
            CertificateCourseTitle = x.CertificateCourseTitle,
            CertificateAddress = x.CertificateAddress,
            CertificateDate = x.CertificateDate
        })
        .FirstOrDefaultAsync();
            if (trainingDto != null)
            {
                return trainingDto;
            }
            else
            {
                return null;
            }
        }

        public async Task RemoveTraining(long id)
        {
            try
            {
                var data = await _context.Certificates.Where(x => x.TrainingId == id).ToListAsync();
                foreach (var entity in data)
                {
                    _context.Certificates.Remove(entity);
                }

            }
            catch (Exception ex)
            {

            }

            try
            {
                var data = await _context.DialyActivities.Where(x => x.TrainingId == id).ToListAsync();
                foreach (var entity in data)
                {
                    _context.DialyActivities.Remove(entity);
                }

            }
            catch (Exception ex)
            {

            }

            try
            {
                var data = await _context.Settings.Where(x => x.TrainingId == id).ToListAsync();
                foreach (var entity in data)
                {
                    _context.Settings.Remove(entity);
                }

            }
            catch (Exception ex)
            {

            }


            try
            {
                var data = await _context.Sponsors.Where(x => x.TrainingId == id).ToListAsync();
                foreach (var entity in data)
                {
                    _context.Sponsors.Remove(entity);
                }

            }
            catch (Exception ex)
            {

            }


            try
            {
                var data = await _context.TestCategories.Where(x => x.TrainingId == id).ToListAsync();
                foreach (var entity in data)
                {
                    _context.TestCategories.Remove(entity);
                }

            }
            catch (Exception ex)
            {

            }



            try
            {
                var data = await _context.TrainingFacilitators.Where(x => x.TrainingId == id).ToListAsync();
                foreach (var entity in data)
                {
                    _context.TrainingFacilitators.Remove(entity);
                }

            }
            catch (Exception ex)
            {

            }


            try
            {
                var data = await _context.TrainingParticipants.Where(x => x.TrainingId == id).ToListAsync();
                foreach (var entity in data)
                {
                    _context.TrainingParticipants.Remove(entity);
                }

            }
            catch (Exception ex)
            {

            }



            try
            {
                var data = await _context.Trainings.Where(x => x.Id == id).ToListAsync();
                foreach (var entity in data)
                {
                    _context.Trainings.Remove(entity);
                }

            }
            catch (Exception ex)
            {

            }



            await _context.SaveChangesAsync();
        }

        public async Task<TrainingDto> GetTrainingByIdReport(long id)
        {
            var trainingDto = await _context.Trainings
                .Include(x=>x.TrainingCategory)
                .Include(x=>x.TrainingFacilitators).ThenInclude(x=>x.User)
                .Include(x=>x.TrainingParticipants).ThenInclude(x => x.User)
                .Include(x=>x.Sponsors)
        .Where(x => x.Id == id)
        .Select(x => new TrainingDto
        {
            Id = x.Id,
            Title = x.Title,
            Address = x.Address,
            State = x.State,
            LGA = x.LGA,
            Category = x.TrainingCategory.Title,
            Ward = x.Ward,
            EnablePostTest = x.EnablePostTest,
            EnablePreTest = x.EnablePreTest,
            PostTestStartTime = x.PostTestStartTime,
            StartDate = x.StartDate,
            PreTestInstruction = x.PreTestInstruction,
            PostTestInstruction = x.PostTestInstruction,
            EndDate = x.EndDate,
            TrainingStatus = x.TrainingStatus,
            DialyStartTime = x.DialyStartTime,
            DialyEndTime = x.DialyEndTime,
            SignInStartTime = x.SignInStartTime,
            SignInStopTime = x.SignInStopTime,
            SignOutStartTime = x.SignOutStartTime,
            SignOutStopTime = x.SignOutStopTime,
            Sponsors = x.Sponsors.Count(),
            TrainingFacilitators = x.TrainingFacilitators.Count(),
            TrainingParticipants = x.TrainingParticipants.Where(x => x.ParticipantTrainingStatus == EnumStatus.ParticipantTrainingStatus.Active).Count(),
            TrainingFacilitatorsList = x.TrainingFacilitators.ToList(),
            TrainingParticipantsList = x.TrainingParticipants.Where(x => x.ParticipantTrainingStatus == EnumStatus.ParticipantTrainingStatus.Active).ToList(),
            SponsorsList = x.Sponsors.ToList(),
            DialyActivities = x.DialyActivities.Count(),
            TestCategory = x.TestCategory.Count(),

            CertificateUseRightSidePhysicalSignature = x.CertificateUseRightSidePhysicalSignature,
            CertificateRightSideSignatureUrl = x.CertificateRightSideSignatureUrl,
            CertificateRightSideSignatureKey = x.CertificateRightSideSignatureKey,
            CertificateRightSideName = x.CertificateRightSideName,
            CertificateRightSideOfficePosition = x.CertificateRightSideOfficePosition,
            CertificateRightSideOfficeTitle = x.CertificateRightSideOfficeTitle,
            CertificateUseLeftSidePhysicalSignature = x.CertificateUseLeftSidePhysicalSignature,
            CertificateLeftSideSignatureUrl = x.CertificateLeftSideSignatureUrl,
            CertificateLeftSideSignatureKey = x.CertificateLeftSideSignatureKey,
            CertificateLeftSideName = x.CertificateLeftSideName,
            CertificateLeftSideOfficePosition = x.CertificateLeftSideOfficePosition,
            CertificateLeftSideOfficeTitle = x.CertificateLeftSideOfficeTitle,
            CertificateTitle = x.CertificateTitle,
            CertificateCourseTitle = x.CertificateCourseTitle,
            CertificateAddress = x.CertificateAddress,
            CertificateDate = x.CertificateDate
        })
        .FirstOrDefaultAsync();
            if (trainingDto != null)
            {
                return trainingDto;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<TrainingDto>> GetTrainingByReportList()
        {
            var trainingDtoList = await _context.Trainings
                .Include(x => x.TrainingCategory)
                .Include(x => x.TrainingFacilitators).ThenInclude(x => x.User)
                .Include(x => x.TrainingParticipants).ThenInclude(x => x.User)
                .Include(x => x.Sponsors)
                .Where(x => x.TrainingStatus != EnumStatus.TrainingStatus.Nill) // You can modify the condition if necessary to fetch multiple trainings.
                .Select(x => new TrainingDto
                {
                    Id = x.Id,
                    CategoryId = x.TrainingCategoryId,
                    Title = x.Title,
                    Address = x.Address,
                    State = x.State,
                    LGA = x.LGA,
                    Category = x.TrainingCategory.Title,
                    CategoryAbbreviation = x.TrainingCategory.Abbreviation,
                    Ward = x.Ward,
                    EnablePostTest = x.EnablePostTest,
                    EnablePreTest = x.EnablePreTest,
                    PostTestStartTime = x.PostTestStartTime,
                    StartDate = x.StartDate,
                    PreTestInstruction = x.PreTestInstruction,
                    PostTestInstruction = x.PostTestInstruction,
                    EndDate = x.EndDate,
                    TrainingStatus = x.TrainingStatus,
                    DialyStartTime = x.DialyStartTime,
                    DialyEndTime = x.DialyEndTime,
                    SignInStartTime = x.SignInStartTime,
                    SignInStopTime = x.SignInStopTime,
                    SignOutStartTime = x.SignOutStartTime,
                    SignOutStopTime = x.SignOutStopTime,
                    Sponsors = x.Sponsors.Count(),
                    TrainingFacilitators = x.TrainingFacilitators.Where(p => p.FacilitatorTrainingStatus == EnumStatus.FacilitatorTrainingStatus.Active).Count(),
                    TrainingParticipants = x.TrainingParticipants
                        .Where(p => p.ParticipantTrainingStatus == EnumStatus.ParticipantTrainingStatus.Active).Count(),
                    TrainingFacilitatorsList = x.TrainingFacilitators.Where(p => p.FacilitatorTrainingStatus == EnumStatus.FacilitatorTrainingStatus.Active).ToList(),
                    TrainingParticipantsList = x.TrainingParticipants
                        .Where(p => p.ParticipantTrainingStatus == EnumStatus.ParticipantTrainingStatus.Active).ToList(),
                    SponsorsList = x.Sponsors.ToList(),
                    DialyActivities = x.DialyActivities.Count(),
                    TestCategory = x.TestCategory.Count(),

                    CertificateUseRightSidePhysicalSignature = x.CertificateUseRightSidePhysicalSignature,
                    CertificateRightSideSignatureUrl = x.CertificateRightSideSignatureUrl,
                    CertificateRightSideSignatureKey = x.CertificateRightSideSignatureKey,
                    CertificateRightSideName = x.CertificateRightSideName,
                    CertificateRightSideOfficePosition = x.CertificateRightSideOfficePosition,
                    CertificateRightSideOfficeTitle = x.CertificateRightSideOfficeTitle,
                    CertificateUseLeftSidePhysicalSignature = x.CertificateUseLeftSidePhysicalSignature,
                    CertificateLeftSideSignatureUrl = x.CertificateLeftSideSignatureUrl,
                    CertificateLeftSideSignatureKey = x.CertificateLeftSideSignatureKey,
                    CertificateLeftSideName = x.CertificateLeftSideName,
                    CertificateLeftSideOfficePosition = x.CertificateLeftSideOfficePosition,
                    CertificateLeftSideOfficeTitle = x.CertificateLeftSideOfficeTitle,
                    CertificateTitle = x.CertificateTitle,
                    CertificateCourseTitle = x.CertificateCourseTitle,
                    CertificateAddress = x.CertificateAddress,
                    CertificateDate = x.CertificateDate
                })
                .OrderByDescending(x=>x.StartDate)
                //.Take(3)
                .ToListAsync();

            return trainingDtoList;
        }

    }
}
