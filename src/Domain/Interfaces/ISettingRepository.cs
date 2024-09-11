using Domain.GenericInterface;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISettingRepository : IRepository<Setting>
    {
        Task<Setting> GetSetting(long trainingId);
        //Task<bool> UpdateSetting(Setting setting, IFormFile signatureRight, IFormFile signatureLeft);
    }
}
