using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IFavoriteRepository : IRepository<Favorite>
    {
        Task<PaginatedResultSet<Favorite>> GetFavoriteByUser(int userId);
        Task<Favorite> GetFavorite(int userId, int movieId);
    }
}
