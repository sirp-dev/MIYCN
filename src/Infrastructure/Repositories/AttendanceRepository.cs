using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.GenericRepository;
using Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Infrastructure.Repositories
{
    public sealed class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
    {
        private readonly AppDbContext _context;

        public AttendanceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Attendance>> GetAttendanceByActivity(long activityId)
        {
            var list = await _context.Attendances
                .Include(x => x.User)
                .Where(x => x.DialyActivityId == activityId).ToListAsync();
            return list.Where(x => x.User != null).ToList();
        }

        public async Task<List<Attendance>> GetAttendanceByTraining(long trainingId)
        {
            var list = await _context.Attendances
                 .Include(x => x.User)
                .Include(x => x.DialyActivity).Where(x => x.DialyActivity.TrainingId == trainingId).ToListAsync();
            return list;
        }
        public async Task ValidateUserToTrainingAttendance(long trainingId)
        {
            // Retrieve the training
            var training = await _context.Trainings.FindAsync(trainingId);
            if (training == null)
            {
                throw new ArgumentException("Invalid training ID");
            }

            // Get all participants of the training
            var participants = await _context.TrainingParticipants
                .Where(tp => tp.TrainingId == trainingId && tp.ParticipantTrainingStatus == ParticipantTrainingStatus.Active)
                .Include(tp => tp.User)
                .ToListAsync();

            // Get all facilitators of the training
            var facilitators = await _context.TrainingFacilitators
                .Where(tp => tp.TrainingId == trainingId && tp.FacilitatorTrainingStatus == FacilitatorTrainingStatus.Active)
                .Include(tp => tp.User)
                .ToListAsync();

            //check null
            participants = participants.Where(x => x.User != null).ToList();
            facilitators = facilitators.Where(x => x.User != null).ToList();



            // Get the daily activities associated with the training
            var dailyActivities = await _context.DialyActivities
                .Where(da => da.TrainingId == trainingId)
                .Include(da => da.Attendances)
                .ToListAsync();

            // Helper function to add attendance
            void AddAttendance(string userId, int accountType, DialyActivity activity)
            {
                var existingAttendance = activity.Attendances
                    .FirstOrDefault(a => a.UserId == userId && a.DialyActivityId == activity.Id);

                if (existingAttendance == null)
                {
                    activity.Attendances.Add(new Attendance
                    {
                        UserId = userId,
                        DialyActivityId = activity.Id,
                        AttendanceSignInStatus = AttendanceSignInStatus.Null, // or any other status
                        AttendanceSignOutStatus = AttendanceSignOutStatus.Null,
                        AccountType = accountType
                    });
                }
                //else
                //{
                //    if (accountType == 2)
                //    {
                //        existingAttendance.AccountType = accountType;
                //        _context.Attach(existingAttendance).State = EntityState.Modified;
                //    }
                //}
            }

            // Iterate over each participant and add attendance
            foreach (var participant in participants)
            {
                foreach (var activity in dailyActivities)
                {
                    AddAttendance(participant.UserId, 1, activity);
                }
            }

            // Iterate over each facilitator and add attendance
            foreach (var facilitator in facilitators)
            {
                foreach (var activity in dailyActivities)
                {
                    AddAttendance(facilitator.UserId, 2, activity);
                }
            }

            await _context.SaveChangesAsync(); // Save changes to the database
        }

        //public async Task ValidateUserToTrainingAttendance(long trainingId)
        //{
        //    // Retrieve the training
        //    var training = await _context.Trainings.FindAsync(trainingId);
        //    if (training == null)
        //    {
        //        throw new ArgumentException("Invalid training ID");
        //    }

        //    // Get all participants of the training
        //    var participants = await _context.TrainingParticipants
        //        .Where(tp => tp.TrainingId == trainingId && tp.ParticipantTrainingStatus == ParticipantTrainingStatus.Active)
        //        .Include(tp => tp.User)
        //        .ToListAsync();

        //    var facilitators = await _context.TrainingFacilitators
        //       .Where(tp => tp.TrainingId == trainingId && tp.FacilitatorTrainingStatus == FacilitatorTrainingStatus.Active)
        //       .Include(tp => tp.User)
        //       .ToListAsync();

        //    // Get the daily activities associated with the training
        //    var dailyActivities = await _context.DialyActivities
        //        .Where(da => da.TrainingId == trainingId)
        //        .Include(da => da.Attendances)
        //        .ToListAsync();

        //    // Iterate over each participant
        //    foreach (var participant in participants)
        //    {
        //        // Check if the participant has already attended the activity on a previous date
        //        foreach (var activity in dailyActivities)
        //        {
        //            // Skip activities whose date has already passed
        //            //if (activity.Date <= DateTime.Today)
        //            //{
        //            //    Console.WriteLine($"Activity on {activity.Date} has already passed.");
        //            //    continue;
        //            //}

        //            var checkexistance = activity.Attendances
        //                .FirstOrDefault(a => a.UserId == participant.UserId && a.DialyActivityId == activity.Id);

        //            if (checkexistance != null)
        //            {
        //                // Participant has attended the activity on a previous date
        //                Console.WriteLine($"User {participant.User.UserName} has already attended activity on {checkexistance.DialyActivity.Date}");
        //                continue; // Skip adding this participant to the attendance
        //            }

        //            // Participant has not attended the activity on any previous date, add to attendance
        //            activity.Attendances.Add(new Attendance
        //            {
        //                UserId = participant.UserId,
        //                DialyActivityId = activity.Id,
        //                AttendanceSignInStatus = AttendanceSignInStatus.Null, // or any other status
        //                AttendanceSignOutStatus = AttendanceSignOutStatus.Null,
        //                AccountType = 1
        //            });
        //        }
        //    }

        //    await _context.SaveChangesAsync(); // Save changes to the database
        //}

        public async Task UpdateSignInAttendanceStatus(List<(long attendanceId, AttendanceSignInStatus status)> attendanceData)
        {
            foreach (var (attendanceId, status) in attendanceData)
            {
                // Retrieve the Attendance record by its ID
                var attendance = await _context.Attendances.FindAsync(attendanceId);
                if (attendance != null)
                {
                    // Update the AttendanceStatus
                    attendance.AttendanceSignInStatus = status;
                    attendance.SignInSubmitted = true;
                    _context.Attach(attendance).State = EntityState.Modified;

                }
                else
                {
                    // Handle the case where the provided attendanceId is not found
                    throw new ArgumentException($"Attendance record with ID {attendanceId} not found.");
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSignOutAttendanceStatus(List<(long attendanceId, AttendanceSignOutStatus status)> attendanceData)
        {
            foreach (var (attendanceId, status) in attendanceData)
            {
                // Retrieve the Attendance record by its ID
                var attendance = await _context.Attendances.FindAsync(attendanceId);
                if (attendance != null)
                {
                    // Update the AttendanceStatus
                    attendance.AttendanceSignOutStatus = status;
                    attendance.SignOutSubmitted = true;
                    _context.Attach(attendance).State = EntityState.Modified;

                }
                else
                {
                    // Handle the case where the provided attendanceId is not found
                    throw new ArgumentException($"Attendance record with ID {attendanceId} not found.");
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAttendanceByActivityId(long activityId)
        {
            try
            {
                var attendance = await _context.Attendances
                    .Where(da => da.DialyActivityId == activityId)
                    .ToListAsync();

                foreach (var item in attendance)
                {
                    _context.Attendances.Remove(item);

                }
                await _context.SaveChangesAsync();
            }
            catch (Exception c) { }
        }
    }
}
