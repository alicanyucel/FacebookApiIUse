using FacebookApiIUse.Services;
using Microsoft.AspNetCore.Mvc;

namespace FacebookApiIUse.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class FacebookController : ControllerBase
{
    private readonly FacebookService _facebookService;

    public FacebookController(FacebookService facebookService)
    {
        _facebookService = facebookService;
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUser([FromQuery] string accessToken)
    {
        if (string.IsNullOrEmpty(accessToken))
        {
            return BadRequest("Access token is required.");
        }

        var userProfile = await _facebookService.GetFacebookUserProfileAsync(accessToken);
        return Ok(userProfile);
    }
    [HttpPost("upload-photo")]
    public async Task<IActionResult> UploadPhoto([FromQuery] string accessToken, [FromQuery] string imageUrl, [FromQuery] string caption)
    {
        if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(imageUrl))
            return BadRequest("Access token and image URL are required.");

        var result = await _facebookService.UploadPhotoAsync(accessToken, imageUrl, caption);
        return Ok(result);
    }
}
