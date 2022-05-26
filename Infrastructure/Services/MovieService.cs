using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieDetailsModel> GetMovieDetails(int movieId)
        {
            var movie = await _movieRepository.GetById(movieId);
            if(movie == null)
            {
                return null;
            }
            var movieDetails = new MovieDetailsModel()
            {
                Id = movie.Id,
                Budget = movie.Budget,
                Overview = movie.Overview, 
                Price = movie.Price,
                PosterUrl = movie.PosterUrl,
                Revenue = movie.Revenue,
                ReleaseDate = movie.ReleaseDate,
                Tagline = movie.Tagline,
                Title = movie.Title,
                RunTime = movie.RunTime,
                BackdropUrl = movie.BackdropUrl,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
            };
            
            foreach(var trailer in movie.Trailer )
            {
                movieDetails.Trailers.Add(new TrailerModel { Id = trailer.Id, Name = trailer.Name, TrailerUrl = trailer.TrailerUrl});
            }

            foreach(var genre in movie.MovieGenre)
            {
                movieDetails.Genres.Add(new GenreModel { Id = genre.GenreId, Name = genre.Genre.Name });
            }

            foreach(var cast in movie.MovieCast)
            {
                movieDetails.Casts.Add(new CastModel { Id = cast.CastId, Name = cast.Cast.Name, Character = cast.Character, ProfilePath = cast.Cast.ProfilePath });
            }

            int counts = 0;
            decimal sum = 0;
            foreach(var review in movie.Review)
            {
                sum = sum + review.Rating;
                counts++;
            }
            movieDetails.Rating = Math.Round((decimal)sum / counts, 2);

            return movieDetails;
        }

        public async Task<PaginatedResultSet<MovieCardModel>> GetMoviesByGenrePaginationd(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovies = await _movieRepository.GetMoviesByGenre(genreId, pageSize, pageNumber);
            var movieCards = new List<MovieCardModel>();

            movieCards.AddRange(pagedMovies.Data.Select(x => new MovieCardModel
            {
                Id = x.Id,
                PosterUrl = x.PosterUrl,
                Title = x.Title,
            }));
            return new PaginatedResultSet<MovieCardModel>(movieCards, pageNumber, pageSize, pagedMovies.Count);
        }

        public async Task<PaginatedResultSet<MovieCardModel>> GetMoviesByUserPaginationd(int userId, int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovies = await _movieRepository.GetMoviesByGenre(userId, pageSize, pageNumber);
            var movieCards = new List<MovieCardModel>();

            movieCards.AddRange(pagedMovies.Data.Select(x => new MovieCardModel
            {
                Id = x.Id,
                PosterUrl = x.PosterUrl,
                Title = x.Title,
            }));
            return new PaginatedResultSet<MovieCardModel>(movieCards, pageNumber, pageSize, pagedMovies.Count);
        }

        public async Task<List<MovieCardModel>> GetTop30GrossingMovies()
        {
            var movies = await _movieRepository.GetTop30GrossingMovies();
            var MovieCards = new List<MovieCardModel>();

            foreach (var movie in movies)
            {
                MovieCards.Add(new MovieCardModel
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        PosterUrl = movie.PosterUrl,
                    }
                ); 
            }
            return MovieCards;
        }
    }
}
