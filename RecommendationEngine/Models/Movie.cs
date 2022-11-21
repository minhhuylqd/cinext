namespace RecommendationEngine.Models;

public record class Movie(
  string? poster_path, 
  bool adult, 
  string overview, 
  string release_date,
  int id,
  string original_title,
  string original_language,
  string title,
  string? backdrop_path,
  double popularity,
  int vote_count,
  bool video,
  double vote_average
);

public record class MovieResponse(
  Movie[] results
);

