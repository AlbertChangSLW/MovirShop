using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserSerivce : IUserService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IReviewRepository _reviewRepository;

        public UserSerivce(IPurchaseRepository purchaseRepository, IMovieRepository movieRepository, IFavoriteRepository favoriteRepository, IReviewRepository reviewRepository)
        {
            _purchaseRepository = purchaseRepository;
            _movieRepository = movieRepository;
            _favoriteRepository = favoriteRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<bool> FavoriteExists(FavoriteRequestModel favoriteRequest, int userId)
        {
            var favorite = await _favoriteRepository.GetFavoriteByUser(userId);
            foreach (Favorite favoriteItem in favorite.Data)
            {
                if (favoriteItem.MovieId == favoriteRequest.MovieId)
                    return true;
            }
            return false;
        }

        public async Task<PaginatedResultSet<MovieCardModel>> GetAllFavoritesForUser(int userId, int pageSize = 30, int pageNumber = 1)
        {
            var pagePurchases = await _movieRepository.GetMoviesByFavorite(userId, pageSize, pageNumber);
            var movieCards = new List<MovieCardModel>();
            movieCards.AddRange(pagePurchases.Data.Select(x => new MovieCardModel
            {
                Id = x.Id,
                PosterUrl = x.PosterUrl,
                Title = x.Title
            }));
            return new PaginatedResultSet<MovieCardModel>(movieCards, pageNumber, pageSize, pagePurchases.Count);
        }
        public async Task AddFavorite(FavoriteRequestModel favoriteRequest, int userId)
        {
            var favorite = new Favorite
            {
                UserId = userId,
                MovieId = favoriteRequest.MovieId
            };
            var createdPurchase = await _favoriteRepository.Add(favorite);
        }

        //public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest, int userId)
        public async Task RemoveFavorite(int movieId, int userId)
        {
            var favorite = await _favoriteRepository.GetFavorite(movieId, userId);
            /*var favorite = new Favorite
            {
                UserId = userId,
                MovieId = favoriteRequest.MovieId
            };*/
            var removePurchase = await _favoriteRepository.Delete(favorite);
        }

        public async Task<ReviewRequestModel> MovieReviewByUser(int userId, int movieId)
        {
            var review = await _reviewRepository.GetReviewByUser(userId, movieId);

            if(review != null)
            {
                var reviewrs = new ReviewRequestModel()
                {
                    UserId = review.UserId,
                    MovieId = review.MovieId,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                };

                return reviewrs;
            }

            return null;            
        }

        public async Task<PaginatedResultSet<ReviewListModel>> GetAllReviewForMovie(int movieId, int pageSize = 50, int pageNumber = 1)
        {
            var reviews = await _reviewRepository.GetAllReviewForMovie(movieId, pageSize, pageNumber);
            var reviewList = new List<ReviewListModel>();

            reviewList.AddRange(reviews.Data.Select(x => new ReviewListModel 
            {
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                Rating = x.Rating,
                ReviewText = x.ReviewText,
                MovieId = x.MovieId
                
            }));
            
            return new PaginatedResultSet<ReviewListModel>(reviewList, pageNumber, pageSize, reviews.Count);
        }
        public async Task AddMovieRevies(ReviewRequestModel reviewRequest)
        {
            var review = new Review
            {
                UserId = reviewRequest.UserId,
                MovieId = reviewRequest.MovieId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText
            };
            var createdPurchase = await _reviewRepository.Add(review);
        }

        public async Task DeleteMovieRevies(ReviewRequestModel reviewRequest)
        {
            var review = await _reviewRepository.GetReviewByUser(reviewRequest.UserId, reviewRequest.MovieId);

            var deleteReview = await _reviewRepository.Delete(review);
        }

        public async Task UpdateMovieRevies(ReviewRequestModel reviewRequest)
        {
            var review = await _reviewRepository.GetReviewByUser(reviewRequest.UserId, reviewRequest.MovieId);

            review.Rating = reviewRequest.Rating;
            review.ReviewText = reviewRequest.ReviewText;

            var updateReview = await _reviewRepository.Update(review);
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var purchases = await _purchaseRepository.GetPurchasesByUser(userId);
            foreach (var purchase in purchases.Data)
            {
                if (purchase.MovieId == purchaseRequest.MovieId)
                    return true;
            }
            return false;
        }

        public async Task<PaginatedResultSet<MovieCardModel>> GetAllPurchasesForUser(int userId, int pageSize = 30, int pageNumber = 1)
        {
            var pagePurchases = await _movieRepository.GetMoviesByPurchase(userId, pageSize, pageNumber);
            var movieCards = new List<MovieCardModel>();
            movieCards.AddRange(pagePurchases.Data.Select(x => new MovieCardModel
            {
                Id = x.Id,
                PosterUrl = x.PosterUrl,
                Title = x.Title
            }));
            return new PaginatedResultSet<MovieCardModel>(movieCards, pageNumber, pageSize, pagePurchases.Count);
        }

        public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
                var purchase = new Purchase
                {
                    UserId = userId,
                    TotalPrice = (decimal)purchaseRequest.Price,
                    PurchaseNumber = purchaseRequest.PurchaseNumber,
                    PurchaseDateTime = DateTime.Now,
                    MovieId = purchaseRequest.MovieId
                };
            var createdPurchase = await _purchaseRepository.Add(purchase);
        }
    }
}
