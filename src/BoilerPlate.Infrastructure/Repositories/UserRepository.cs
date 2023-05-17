using BoilerPlate.Domain.Interfaces.Repositories;
using BoilerPlate.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BoilerPlate.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BoilerPlateDbContext _dbContext;

        public UserRepository(BoilerPlateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(User user)
        {
            _dbContext.Users.Add(user);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users.Include(u => u.Contacts).ToListAsync();
        }

        public async Task<User> GetByContactNumberAsync(string number)
        {
            return await _dbContext.Users.Include(u => u.Contacts).Where(u => u.Contacts.Any(c => c.Number == number)).FirstOrDefaultAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _dbContext.Users.Include(u => u.Contacts).Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<UserContact>> GetContactListByIdAsync(Guid id)
        {
            return await _dbContext.Users.Where(u => u.Id == id).SelectMany(u => u.Contacts).ToListAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Update(User user)
        {
            _dbContext.Users.Update(user);
        }
    }
}
