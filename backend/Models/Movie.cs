using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models
{
public class Movie
{
    [BsonId]
    public ObjectId Id { get; set; }  // MongoDB automatically generates this ID

    [BsonElement("name")]
    public string? Name { get; set; }  // Movie Name

    [BsonElement("description")]
    public string? Description { get; set; }  // What the user thinks about the movie

    [BsonElement("year")]
    public int Year { get; set; }  // Watched Year

    [BsonElement("imdb")]
    public double ImdbRating { get; set; }  // IMDb Rating (out of 10)

    [BsonElement("rating")]
    public double UserRating { get; set; }  // User Rating (out of 10)

    [BsonElement("poster")]
    public string? PosterUrl { get; set; }  // Poster URL or file reference
}
}
