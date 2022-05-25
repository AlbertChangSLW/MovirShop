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

        public UserSerivce(IPurchaseRepository purchaseRepository, IMovieRepository movieRepository, IFavoriteRepository favoriteRepository)
        {
            _purchaseRepository = purchaseRepository;
            _movieRepository = movieRepository;
            _favoriteRepository = favoriteRepository;
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorite = new Favorite
            {
                UserId = favoriteRequest.UserId,
                MovieId = favoriteRequest.MovieId
            };
            var createdPurchase = await _favoriteRepository.Add(favorite);
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


    }
}
