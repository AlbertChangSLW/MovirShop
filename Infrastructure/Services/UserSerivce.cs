﻿using ApplicationCore.Contracts.Repositories;
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

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest, int userId)
        {
            var favorite = new Favorite
            {
                UserId = userId,
                MovieId = favoriteRequest.MovieId
            };
            var createdPurchase = await _favoriteRepository.Add(favorite);
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

        public async Task DeleteMovieRevies(int userId, int movieId)
        {
            var reviewList = await _reviewRepository.GetReviewByUser(userId);
            foreach(var review in reviewList.Data)
            {
                if(review.MovieId == movieId)
                {
                    var deleteItem = new Review { MovieId = review.MovieId, UserId = review.UserId, Rating = review.Rating, ReviewText = review.ReviewText};
                    var deleteReview = await _reviewRepository.Delete(deleteItem);
                }
            }
        }

        public async Task<bool> FavoriteExists(FavoriteRequestModel favoriteRequest, int userId)
        {
            var favorite = await _favoriteRepository.GetFavoriteByUser(userId);
            foreach(Favorite favoriteItem in favorite.Data)
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

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var purchases = await _purchaseRepository.GetPurchasesByUser(userId);
            foreach(var purchase in purchases.Data)
            {
                if (purchase.MovieId == purchaseRequest.MovieId)
                    return true;
            }
            return false;
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

        //public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest, int userId)
        public async Task RemoveFavorite(int movieId, int userId) 
        {
            var favorite = await _favoriteRepository.GetFavorite(movieId, userId);
            /*var favorite = new Favorite
            {
                UserId = userId,
                MovieId = favoriteRequest.MovieId
            };*/
            var createdPurchase = await _favoriteRepository.Delete(favorite);
        }

        public async Task UpdateMovieRevies(ReviewRequestModel reviewRequest)
        {
            var reviewList = await _reviewRepository.GetReviewByUser(reviewRequest.UserId);
            foreach (var review in reviewList.Data)
            {
                if (review.MovieId == reviewRequest.MovieId)
                {
                    var updateReview = new Review { MovieId = reviewRequest.MovieId, UserId = reviewRequest.UserId, Rating = reviewRequest.Rating, ReviewText = reviewRequest.ReviewText };
                    var deleteReview = await _reviewRepository.Update(updateReview);
                }
            }
        }
    }
}
