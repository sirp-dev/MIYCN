using Domain.GenericInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Domain.Interfaces
{
    public interface IAttendanceRepository : IRepository<Attendance>
    {
        Task<List<Attendance>> GetAttendanceByActivity(long activityId);
        Task<List<Attendance>> GetAttendanceByTraining(long trainingId);
        Task ValidateUserToTrainingAttendance(long trainingId);
        Task UpdateSignInAttendanceStatus(List<(long attendanceId, AttendanceSignInStatus status)> attendanceData);
        Task UpdateSignOutAttendanceStatus(List<(long attendanceId, AttendanceSignOutStatus status)> attendanceData);

        Task DeleteAttendanceByActivityId(long activityId);
    }
}
