using DotnetCouchbaseExample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class QuestionController : ControllerBase
{
    private readonly ICouchbaseService _couchbaseService;

    public QuestionController(ICouchbaseService couchbaseService)
    {
        _couchbaseService = couchbaseService;
    }

    [HttpPost("create-question")]
    public async Task<IActionResult> CreateQuestion([FromQuery] string userId, [FromQuery] string presentationId, [FromQuery] string slideId, [FromBody] Question question)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("UserId is required.");
        }

        if (string.IsNullOrEmpty(presentationId))
        {
            return BadRequest("PresentationId is required.");
        }

        if (string.IsNullOrEmpty(slideId))
        {
            return BadRequest("SlideId is required.");
        }

        var userResult = await _couchbaseService.GetAsync(userId);
        var userInfo = userResult.ContentAs<UserInfo>();
        if (userInfo == null)
        {
            return NotFound("User not found.");
        }

        var slide = userInfo.Presentations
            .FirstOrDefault(p => p.Id == presentationId)?
            .Slides.FirstOrDefault(s => s.Id == slideId);

        if (slide == null)
        {
            return NotFound("Slide not found.");
        }

        question.Id = Guid.NewGuid().ToString();
        slide.Questions = slide.Questions?.Append(question).ToArray() ?? new[] { question };

        await _couchbaseService.UpsertAsync(userId, userInfo);

        return CreatedAtAction(nameof(GetQuestionById), new { userId = userId, presentationId = presentationId, slideId = slideId, id = question.Id }, question);
    }

    [HttpGet("{userId}/{presentationId}/{slideId}/{id}")]
    public async Task<IActionResult> GetQuestionById(string userId, string presentationId, string slideId, string id)
    {
        var user = await _couchbaseService.GetAsync(userId);
        if (user.ContentAs<UserInfo>() == null)
        {
            return NotFound();
        }

        var question = user.ContentAs<UserInfo>().Presentations
            .FirstOrDefault(p => p.Id == presentationId)?
            .Slides.FirstOrDefault(s => s.Id == slideId)?
            .Questions.FirstOrDefault(q => q.Id == id);

        if (question == null)
        {
            return NotFound();
        }

        return Ok(question);
    }

    [HttpPut("{userId}/{presentationId}/{slideId}/{id}")]
    public async Task<IActionResult> UpdateQuestion(string userId, string presentationId, string slideId, string id, [FromBody] Question updatedQuestion)
    {
        var userResult = await _couchbaseService.GetAsync(userId);
        var userInfo = userResult.ContentAs<UserInfo>();
        if (userInfo == null)
        {
            return NotFound("User not found.");
        }

        var slide = userInfo.Presentations
            .FirstOrDefault(p => p.Id == presentationId)?
            .Slides.FirstOrDefault(s => s.Id == slideId);

        if (slide == null)
        {
            return NotFound("Slide not found.");
        }

        var questions = slide.Questions;
        var index = Array.FindIndex(questions, q => q.Id == id);
        if (index == -1)
        {
            return NotFound("Question not found.");
        }

        var existingQuestion = questions[index];
        updatedQuestion.Id = existingQuestion.Id; 
        updatedQuestion.Options = existingQuestion.Options; 

        questions[index] = updatedQuestion;
        slide.Questions = questions;

        await _couchbaseService.UpsertAsync(userInfo.Id, userInfo);

        return NoContent();
    }

    [HttpDelete("{userId}/{presentationId}/{slideId}/{id}")]
    public async Task<IActionResult> DeleteQuestion(string userId, string presentationId, string slideId, string id)
    {
        var user = await _couchbaseService.GetAsync(userId);
        if (user.ContentAs<UserInfo>() == null)
        {
            return NotFound();
        }

        var slide = user.ContentAs<UserInfo>().Presentations
            .FirstOrDefault(p => p.Id == presentationId)?
            .Slides.FirstOrDefault(s => s.Id == slideId);

        if (slide == null)
        {
            return NotFound();
        }

        slide.Questions = slide.Questions.Where(q => q.Id != id).ToArray();

        await _couchbaseService.UpsertAsync(user.ContentAs<UserInfo>().Id, user.ContentAs<UserInfo>());

        return NoContent();
    }
}
