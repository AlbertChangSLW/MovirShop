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
        Task<bool> FavoriteExists(FavoriteRequestModel favoriteRequest, int userId);

        Task<PaginatedResultSet<MovieCardModel>> GetAllFavoritesForUser(int userId, int pageSize = 30, int pageNumber = 1);

        //Task<List<PurchasesDetailsModel>> GetPurchasesDetails(int userId, int movieId);
        Task AddFavorite(FavoriteRequestModel favoriteRequest, int userId);

        //Task RemoveFavorite(FavoriteRequestModel favoriteRequest, int userId);
        Task RemoveFavorite(int movieId, int userId);

        Task<ReviewRequestModel> MovieReviewByUser(int userId, int movieId);

        Task<PaginatedResultSet<ReviewListModel>> GetAllReviewForMovie(int movieId, int pageSize = 50, int pageNumber = 1);

        Task AddMovieRevies(ReviewRequestModel reviewRequest);

        Task UpdateMovieRevies(ReviewRequestModel reviewRequestModel);
        Task DeleteMovieRevies(ReviewRequestModel reviewRequestModel);

        Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);

        Task<PaginatedResultSet<MovieCardModel>> GetAllPurchasesForUser(int userId, int pageSize = 30, int pageNumber = 1);

        Task PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
        //Task<List<ReviewModel>> GetAllReviewsForUser(int Id);
    }
}
