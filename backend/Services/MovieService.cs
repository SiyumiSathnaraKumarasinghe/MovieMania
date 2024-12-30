using backend.Models;
using MongoDB.Driver;

namespace backend.Services
{
    public class MovieService
    {
        private readonly IMongoCollection<Movie> _movies;

        public MovieService(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _movies = database.GetCollection<Movie>("movies");
        }

        public List<Movie> GetMovies() => _movies.Find(movie => true).ToList();

        public Movie AddMovie(Movie movie)
        {
            _movies.InsertOne(movie);
            return movie;
        }
    }
}
