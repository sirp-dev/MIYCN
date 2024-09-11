using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDashboardRepository  
    {
        Task<AdminDashboardDto> AdminDashboardData(string? state);
        Task<UserDashboardDto> UserDashboardData(string userId);
    }
}
