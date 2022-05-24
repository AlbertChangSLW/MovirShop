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
        //Task<PaginatedResultSet<MovieCardModel>> GetAllPurchasesForUser(int Id, int pageSize = 30, int pageNumber = 1); 
        //Task<List<PurchasesDetailsModel>> GetPurchasesDetails(int userId, int movieId);
        //Task AddFavorite(FavoriteRequestModel favoriteRequest);
        //Task RemoveFavorite(FavoriteRequestModel favoriteRequest);
        //Task FavoriteExists(int Id, int MovieId);
        //Task<List<MovieCardModel>> GetAllFavoritesForUser(int Id);
        //Task AddMovieRevies(ReviewRequestModel reviewRequestModel);
        //Task UpdateMovieRevies(ReviewRequestModel reviewRequestModel);
        //Task DeleteMovieRevies(ReviewRequestModel reviewRequestModel);
        //Task<List<ReviewModel>> GetAllReviewsForUser(int Id);
    }
}
