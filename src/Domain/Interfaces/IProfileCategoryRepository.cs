using Domain.GenericInterface;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IProfileCategoryRepository : IRepository<ProfileCategory>
    {
        Task<List<ProfileCategory>> GetByUserId(string id);
    }
}
