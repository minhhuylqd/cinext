using Microsoft.AspNetCore.Mvc;
using RecommendationEngine;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase {
  [HttpGet]
  public Movie getMovie() {
    return new Movie("123");
  }
}