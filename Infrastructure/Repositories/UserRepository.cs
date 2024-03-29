﻿using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<User> GetUserById(int Id)
        {
            var user = await _dbContext.User.FindAsync(Id);
            //var user = await _dbContext.User.FirstOrDefaultAsync(x => x.Id == Id);
            return user;
        }
    }
}
