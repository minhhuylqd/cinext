using Microsoft.AspNetCore.Mvc;
using RecommendationEngine.Models;

[ApiController]
[Route("")]
public class HealthCheckController : ControllerBase {
  [HttpGet]
  public HealthCheckResponse response() {
    HealthCheckResponse healthCheckResponse = new HealthCheckResponse{
      healthCheck = "The Server is Healthy and Running"
    };
    return healthCheckResponse;
  }
}