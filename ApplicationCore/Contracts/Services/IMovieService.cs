using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IMovieService
    {
        Task<List<MovieCardModel>> GetTop30GrossingMovies();

        Task<MovieDetailsModel> GetMovieDetails(int movieId);
        Task<PaginatedResultSet<MovieCardModel>> GetMoviesByGenrePaginationd( int genreId, int pageSize = 30, int pageNumber = 1);
        //Task<PaginatedResultSet<MovieCardModel>> GetMoviesByPurchasePaginationd(int userId, int pageSize = 30, int pageNumber = 1);
        Task<PaginatedResultSet<MovieCardModel>> GetAllMoviesPaginationd(int pageSize = 30, int pageNumber = 1);
    }
}
