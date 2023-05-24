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
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        //public async Task<List<Purchase>> GetPurchasesByUser(int id)
        //{
        //    var purchase = await _dbContext.Purchase.Where(x => x.UserId == id).ToListAsync();
        //    return purchase;
        //}

        public async Task<PaginatedResultSet<Purchase>> GetPurchasesByUser(int userId)
        {
            var totalPurchaseCount = await _dbContext.Purchase.Where(m => m.UserId == userId).CountAsync();
            var purchase = await _dbContext.Purchase.OrderByDescending(x => x.Id).Where(x => x.UserId == userId).ToListAsync();
            var purchaseList = new PaginatedResultSet<Purchase>(purchase, 1, 30, totalPurchaseCount);
            return purchaseList;
        }
        //public async Task<PaginatedResultSet<Movie>> GetMoviesByPurchase(int userId, int pageSize = 30, int pageNumber = 1)
        //{
        //    var totalMoviesCount = await _dbContext.Purchase.Where(m => m.UserId == userId).CountAsync();

        //    if (totalMoviesCount == 0)
        //    {
        //        throw new Exception("No Movies Found for that Purchase");
        //    };

        //    var movies = await _dbContext.Purchase.Where(x => x.UserId == userId).Include(x => x.Movie).OrderBy(x => x.MovieId)
        //        .Select(x => new Movie
        //        {
        //            Id = x.MovieId,
        //            PosterUrl = x.Movie.PosterUrl,
        //            Title = x.Movie.Title
        //        })
        //        .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        //    var pagedMovies = new PaginatedResultSet<Movie>(movies, pageNumber, pageSize, totalMoviesCount);
        //    return pagedMovies;
        //}
    }
}
