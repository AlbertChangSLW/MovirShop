using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<Review> GetReviewByUser(int userId, int movieId);
        Task<PaginatedResultSet<Review>> GetAllReviewForMovie(int movieId, int pageSize = 50, int pageNumber = 1);
    }
}
