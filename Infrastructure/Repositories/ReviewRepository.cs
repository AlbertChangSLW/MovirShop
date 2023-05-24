using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Review> GetReviewByUser(int userId, int movieId)
        {
            var review = await _dbContext.Review.FirstOrDefaultAsync(m => m.UserId == userId && m.MovieId == movieId );

            return review;
        }

        public async Task<PaginatedResultSet<Review>> GetAllReviewForMovie(int movieId, int pageSize = 50, int pageNumber = 1)
        {
            var totalReviewCount = await _dbContext.Review.Where(m => m.MovieId == movieId).CountAsync();

            var reviews = await _dbContext.Review.Where(x => x.MovieId == movieId).OrderBy(x => x.UserId).Include(x => x.Movie).Include(x => x.User).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var pagedMovies = new PaginatedResultSet<Review>(reviews, pageNumber, pageSize, totalReviewCount);

            return pagedMovies;
        }
    }
}
