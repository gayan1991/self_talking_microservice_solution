using BoilerPlate.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoilerPlate.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Update(User user);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByContactNumberAsync(string number);
        Task<List<UserContact>> GetContactListByIdAsync(Guid id);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
