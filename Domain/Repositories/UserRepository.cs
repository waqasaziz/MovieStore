using Domain.Model;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> FindByUserNameAsync(string userName);
    }

    public class UserRepository : IUserRepository
    {
        private MovieStoreDbContext _dbContext;

        public UserRepository(MovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> FindByUserNameAsync(string userName)
        {
            return await _dbContext
                                .Users
                                .SingleOrDefaultAsync(x => x.NormalisedUserName == userName.ToUpper());
        }

    }
}
