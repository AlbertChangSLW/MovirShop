using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Movie>> GetTop30GrossingMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(x => x.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public override async Task<Movie> GetById(int id)
        {
            var movie = await _dbContext.Movies.Include(x => x.MovieGenre).ThenInclude(x => x.Genre).Include(x => x.MovieCast).ThenInclude(x =>x.Cast)
                .Include(x => x.Trailer).Include(x => x.Review).FirstOrDefaultAsync(x => x.Id == id);

            return movie;
        }

        public async Task<PaginatedResultSet<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            var totalMoviesCount = await _dbContext.MovieGenre.Where(m => m.GenreId == genreId).CountAsync();

            if(totalMoviesCount == 0)
            {
                throw new Exception("No Movies Found for that genre");
            };

            var movies = await _dbContext.MovieGenre.Where(x => x.GenreId == genreId).Include(x => x.Movie).OrderBy(x => x.MovieId)
                .Select(x => new Movie
                {
                    Id = x.MovieId,
                    PosterUrl = x.Movie.PosterUrl,
                    Title = x.Movie.Title
                })
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var pagedMovies = new PaginatedResultSet<Movie>(movies, pageNumber, pageSize, totalMoviesCount);
            return pagedMovies;
        }
    }
}
