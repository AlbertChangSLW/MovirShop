using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PaginatedResultSet<Review>> GetFavoriteByUser(int userId)
        {
            var totalFavoriteCount = await _dbContext.Review.Where(m => m.UserId == userId).CountAsync();
            var favorite = await _dbContext.Review.OrderByDescending(x => x.Rating).Where(x => x.UserId == userId).ToListAsync();
            var favoriteList = new PaginatedResultSet<Review>(favorite, 1, 30, totalFavoriteCount);
            return favoriteList;
        }
    }
}
