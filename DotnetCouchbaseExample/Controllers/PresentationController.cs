using DotnetCouchbaseExample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PresentationController : ControllerBase
{
    private readonly ICouchbaseService _couchbaseService;

    public PresentationController(ICouchbaseService couchbaseService)
    {
        _couchbaseService = couchbaseService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePresentation([FromQuery] string userId, [FromBody] Presentation presentation)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("UserId is required.");
        }

        var user = await _couchbaseService.GetAsync(userId);
        if (user.ContentAs<UserInfo>() == null)
        {
            return NotFound();
        }

        presentation.Id = System.Guid.NewGuid().ToString();
        var userInfo = user.ContentAs<UserInfo>();
        userInfo.Presentations = userInfo.Presentations?.Append(presentation).ToArray() ?? new[] { presentation };

        await _couchbaseService.UpsertAsync(userInfo.Id, userInfo);

        return CreatedAtAction(nameof(GetPresentationById), new { id = presentation.Id }, presentation);
    }


    [HttpGet("{userId}/{presentationId}")]
    public async Task<IActionResult> GetPresentationById(string userId, string presentationId)
    {
        var user = await _couchbaseService.GetAsync(userId);
        if (user.ContentAs<UserInfo>() == null)
        {
            return NotFound();
        }

        var presentation = user.ContentAs<UserInfo>().Presentations.FirstOrDefault(p => p.Id == presentationId);
        if (presentation == null)
        {
            return NotFound();
        }

        return Ok(presentation);
    }


    [HttpPut("{userId}/{presentationId}")]
    public async Task<IActionResult> UpdatePresentation(string userId, string presentationId, [FromBody] Presentation updatedPresentation)
    {
        var userResult = await _couchbaseService.GetAsync(userId);
        var userInfo = userResult.ContentAs<UserInfo>();

        if (userInfo == null)
        {
            return NotFound("User not found.");
        }

        var presentations = userInfo.Presentations;
        var index = Array.FindIndex(presentations, p => p.Id == presentationId);

        if (index == -1)
        {
            return NotFound("Presentation not found.");
        }

        var currentPresentation = presentations[index];
        updatedPresentation.Id = currentPresentation.Id; 
        updatedPresentation.Slides = currentPresentation.Slides; 

        presentations[index] = updatedPresentation;

        userInfo.Presentations = presentations;

        await _couchbaseService.UpsertAsync(userId, userInfo);

        return NoContent();
    }


    [HttpDelete("{userId}/{presentationId}")]
    public async Task<IActionResult> DeletePresentation(string userId, string presentationId)
    {
        var user = await _couchbaseService.GetAsync(userId);
        if (user.ContentAs<UserInfo>() == null)
        {
            return NotFound();
        }

        var userInfo = user.ContentAs<UserInfo>();
        var presentationToRemove = userInfo.Presentations.FirstOrDefault(p => p.Id == presentationId);
        if (presentationToRemove == null)
        {
            return NotFound();
        }

        userInfo.Presentations = userInfo.Presentations.Where(p => p.Id != presentationId).ToArray();

        await _couchbaseService.UpsertAsync(userInfo.Id, userInfo);
        return NoContent();
    }


}
