using Domain.GenericInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISponsorRepository : IRepository<Sponsor>
    {
        Task<List<Sponsor>> GetAll(long id);   
    }
}
