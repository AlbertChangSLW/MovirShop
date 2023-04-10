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
    public class FavoriteRepository : Repository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PaginatedResultSet<Favorite>> GetFavoriteByUser(int userId)
        {
            var totalFavoriteCount = await _dbContext.Favorite.Where(m => m.UserId == userId).CountAsync();
            var favorite = await _dbContext.Favorite.OrderByDescending( x => x.Id).Where(x => x.UserId == userId).ToListAsync();
            var favoriteList = new PaginatedResultSet<Favorite>(favorite, 1, 30, totalFavoriteCount);
            return favoriteList;
        }

        public async Task<Favorite> GetFavorite(int movieId, int userId)
        {
            var favorite = await _dbContext.Favorite.FirstOrDefaultAsync(x => x.MovieId == movieId && x.UserId == userId);
            return favorite;
        }
    }
}
