using DotnetCouchbaseExample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class SlideController : ControllerBase
{
    private readonly ICouchbaseService _couchbaseService;

    public SlideController(ICouchbaseService couchbaseService)
    {
        _couchbaseService = couchbaseService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSlide([FromQuery] string userId, [FromQuery] string presentationId, [FromBody] Slide slide)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("UserId is required.");
        }

        if (string.IsNullOrEmpty(presentationId))
        {
            return BadRequest("PresentationId is required.");
        }

        var userResult = await _couchbaseService.GetAsync(userId);
        var userInfo = userResult.ContentAs<UserInfo>();
        if (userInfo == null)
        {
            return NotFound("User not found.");
        }

        var presentation = userInfo.Presentations.FirstOrDefault(p => p.Id == presentationId);
        if (presentation == null)
        {
            return NotFound("Presentation not found.");
        }

        slide.Id = Guid.NewGuid().ToString();

        if (presentation.Slides == null)
        {
            presentation.Slides = new Slide[] { slide };
        }
        else
        {
            presentation.Slides = presentation.Slides.Append(slide).ToArray();
        }

        await _couchbaseService.UpsertAsync(userId, userInfo);

        return CreatedAtAction(nameof(GetSlideById), new { userId = userId, presentationId = presentationId, id = slide.Id }, slide);
    }

    [HttpGet("{userId}/{presentationId}/{id}")]
    public async Task<IActionResult> GetSlideById(string userId, string presentationId, string id)
    {
        var user = await _couchbaseService.GetAsync(userId);
        if (user.ContentAs<UserInfo>() == null)
        {
            return NotFound();
        }

        var slide = user.ContentAs<UserInfo>().Presentations
            .FirstOrDefault(p => p.Id == presentationId)?
            .Slides.FirstOrDefault(s => s.Id == id);

        if (slide == null)
        {
            return NotFound();
        }

        return Ok(slide);
    }

    [HttpPut("{userId}/{presentationId}/{id}")]
    public async Task<IActionResult> UpdateSlide(string userId, string presentationId, string id, [FromBody] Slide updatedSlide)
    {
        var userResult = await _couchbaseService.GetAsync(userId);
        var userInfo = userResult.ContentAs<UserInfo>();
        if (userInfo == null)
        {
            return NotFound("User not found.");
        }

        var presentation = userInfo.Presentations.FirstOrDefault(p => p.Id == presentationId);
        if (presentation == null)
        {
            return NotFound("Presentation not found.");
        }

        var slides = presentation.Slides;
        var index = Array.FindIndex(slides, s => s.Id == id);
        if (index == -1)
        {
            return NotFound("Slide not found.");
        }

        var existingSlide = slides[index];
        updatedSlide.Id = existingSlide.Id; 
        updatedSlide.Questions = existingSlide.Questions;

        slides[index] = updatedSlide;
        presentation.Slides = slides;

        await _couchbaseService.UpsertAsync(userId, userInfo);

        return NoContent();
    }


    [HttpDelete("{userId}/{presentationId}/{id}")]
    public async Task<IActionResult> DeleteSlide(string userId, string presentationId, string id)
    {
        var userResult = await _couchbaseService.GetAsync(userId);
        var userInfo = userResult.ContentAs<UserInfo>();
        if (userInfo == null)
        {
            return NotFound("User not found.");
        }

        var presentation = userInfo.Presentations.FirstOrDefault(p => p.Id == presentationId);
        if (presentation == null)
        {
            return NotFound("Presentation not found.");
        }

        var slides = presentation.Slides;
        var slideIndex = Array.FindIndex(slides, s => s.Id == id);
        if (slideIndex == -1)
        {
            return NotFound("Slide not found.");
        }

        var updatedSlides = slides.Where((_, index) => index != slideIndex).ToArray();
        presentation.Slides = updatedSlides;

        await _couchbaseService.UpsertAsync(userId, userInfo);

        return NoContent();
    }
}
