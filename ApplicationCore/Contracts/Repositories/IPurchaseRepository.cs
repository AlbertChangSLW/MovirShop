using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        Task<PaginatedResultSet<Purchase>> GetPurchasesByUser(int userId);
        //Task<PaginatedResultSet<Movie>> GetMoviesByPurchase(int userId, int pageSize = 30, int pageNumber = 1);
        //Task<List<Purchase>> GetPurchasesByUser(int id);
    }
}
