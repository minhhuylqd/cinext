using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RecommendationEngine.Models;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase {

  private readonly IConfiguration _configuration;
  private string apiKey = string.Empty;
  
  public MovieController(IConfiguration configuration) {
    _configuration = configuration;
    apiKey = _configuration.GetSection("AppSettings:ApiKey").Value;
  }

  [HttpGet("{movieQuery}")]
  public async Task<IActionResult> getMovie(string movieQuery) {

    var searchMoviePayload = $"api_key={apiKey}&language=en-US&page=1&include_adult=false&query={movieQuery}";
    var searchMoviePath = $"https://api.themoviedb.org/3/search/movie?{searchMoviePayload}";

    using HttpClient client = new();
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
      new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
    client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

    await using Stream searchStream =
      await client.GetStreamAsync(searchMoviePath);
    var movies =
      await JsonSerializer.DeserializeAsync<MovieResponse>(searchStream);

    var movieId = (movies != null) ? movies.results[0].id : -1;

    var movieRecommendPayload = $"api_key={apiKey}&language=en-US&page=1";
    var movieRecommendPath = $"https://api.themoviedb.org/3/movie/{movieId}/recommendations?{movieRecommendPayload}";

    await using Stream recommendStream =
      await client.GetStreamAsync(movieRecommendPath);
    var recommendedMovies =
      await JsonSerializer.DeserializeAsync<MovieResponse>(recommendStream);

    return Ok(recommendedMovies);
  }
}