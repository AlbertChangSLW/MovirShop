using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IUserService
    {
        Task PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
        Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);
        Task<PaginatedResultSet<MovieCardModel>> GetAllPurchasesForUser(int userId, int pageSize = 30, int pageNumber = 1);
        //Task<List<PurchasesDetailsModel>> GetPurchasesDetails(int userId, int movieId);
        Task AddFavorite(FavoriteRequestModel favoriteRequest);
        Task RemoveFavorite(FavoriteRequestModel favoriteRequest);
        Task<bool> FavoriteExists(int id, int movieId);
        Task<PaginatedResultSet<MovieCardModel>> GetAllFavoritesForUser(int userId, int pageSize = 30, int pageNumber = 1);
        Task AddMovieRevies(ReviewRequestModel reviewRequest);
        Task UpdateMovieRevies(ReviewRequestModel reviewRequestModel);
        Task DeleteMovieRevies(int userId, int movieId);
        //Task<List<ReviewModel>> GetAllReviewsForUser(int Id);
    }
}
