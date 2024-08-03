using DotnetCouchbaseExample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class OptionController : ControllerBase
{
    private readonly ICouchbaseService _couchbaseService;

    public OptionController(ICouchbaseService couchbaseService)
    {
        _couchbaseService = couchbaseService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOption([FromQuery] string userId, [FromQuery] string presentationId, [FromQuery] string slideId, [FromQuery] string questionId, [FromBody] Option option)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(presentationId) || string.IsNullOrEmpty(slideId) || string.IsNullOrEmpty(questionId))
        {
            return BadRequest("All IDs (userId, presentationId, slideId, questionId) are required.");
        }

        var userResult = await _couchbaseService.GetAsync(userId);
        var userInfo = userResult.ContentAs<UserInfo>();
        if (userInfo == null)
        {
            return NotFound("User not found.");
        }

        var question = userInfo.Presentations
            .FirstOrDefault(p => p.Id == presentationId)?
            .Slides.FirstOrDefault(s => s.Id == slideId)?
            .Questions.FirstOrDefault(q => q.Id == questionId);

        if (question == null)
        {
            return NotFound("Question not found.");
        }

        option.Id = Guid.NewGuid().ToString();
        question.Options = question.Options?.Append(option).ToArray() ?? new[] { option };

        await _couchbaseService.UpsertAsync(userId, userInfo);

        return CreatedAtAction(nameof(GetOptionById), new { userId = userId, presentationId = presentationId, slideId = slideId, questionId = questionId, id = option.Id }, option);
    }


    [HttpGet("{userId}/{presentationId}/{slideId}/{questionId}/{id}")]
    public async Task<IActionResult> GetOptionById(string userId, string presentationId, string slideId, string questionId, string id)
    {
        var user = await _couchbaseService.GetAsync(userId);
        if (user.ContentAs<UserInfo>() == null)
        {
            return NotFound();
        }

        var option = user.ContentAs<UserInfo>().Presentations
            .FirstOrDefault(p => p.Id == presentationId)?
            .Slides.FirstOrDefault(s => s.Id == slideId)?
            .Questions.FirstOrDefault(q => q.Id == questionId)?
            .Options.FirstOrDefault(o => o.Id == id);

        if (option == null)
        {
            return NotFound();
        }

        return Ok(option);
    }

    [HttpPut("{userId}/{presentationId}/{slideId}/{questionId}/{id}")]
    public async Task<IActionResult> UpdateOption(string userId, string presentationId, string slideId, string questionId, string id, [FromBody] Option updatedOption)
    {
        var userResult = await _couchbaseService.GetAsync(userId);
        var userInfo = userResult.ContentAs<UserInfo>();

        if (userInfo == null)
        {
            return NotFound("User not found.");
        }

        var question = userInfo.Presentations
            .FirstOrDefault(p => p.Id == presentationId)?
            .Slides.FirstOrDefault(s => s.Id == slideId)?
            .Questions.FirstOrDefault(q => q.Id == questionId);

        if (question == null)
        {
            return NotFound("Question not found.");
        }

        var options = question.Options;
        var index = Array.FindIndex(options, o => o.Id == id);

        if (index == -1)
        {
            return NotFound("Option not found.");
        }

        var currentOption = options[index];
        currentOption.OptionText = updatedOption.OptionText ?? currentOption.OptionText; 
                                                                     

        options[index] = currentOption;
        question.Options = options;

        await _couchbaseService.UpsertAsync(userId, userInfo);

        return NoContent();
    }

    [HttpDelete("{userId}/{presentationId}/{slideId}/{questionId}/{id}")]
    public async Task<IActionResult> DeleteOption(string userId, string presentationId, string slideId, string questionId, string id)
    {
        var user = await _couchbaseService.GetAsync(userId);
        if (user.ContentAs<UserInfo>() == null)
        {
            return NotFound();
        }

        var question = user.ContentAs<UserInfo>().Presentations
            .FirstOrDefault(p => p.Id == presentationId)?
            .Slides.FirstOrDefault(s => s.Id == slideId)?
            .Questions.FirstOrDefault(q => q.Id == questionId);

        if (question == null)
        {
            return NotFound();
        }

        question.Options = question.Options.Where(o => o.Id != id).ToArray();

        await _couchbaseService.UpsertAsync(user.ContentAs<UserInfo>().Id, user.ContentAs<UserInfo>());

        return NoContent();
    }
}
