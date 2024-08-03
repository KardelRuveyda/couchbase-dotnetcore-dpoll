using DotnetCouchbaseExample.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserInfoController : ControllerBase
{
    private readonly ICouchbaseService _couchbaseService;

    public UserInfoController(ICouchbaseService couchbaseService)
    {
        _couchbaseService = couchbaseService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> CreateUser([FromBody] UserInfo userInfo)
    {
        if (userInfo == null)
        {
            return BadRequest("User information is null.");
        }

        userInfo.Id = System.Guid.NewGuid().ToString();
        await _couchbaseService.InsertAsync(userInfo.Id.ToString(), userInfo);
        return Ok("User added successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var result = await _couchbaseService.GetAsync(id);

        if (result.ContentAs<UserInfo>() == null)
        {
            return NotFound();
        }

        return Ok(result.ContentAs<UserInfo>());
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UserInfo updatedUserInfo)
    {
        if (updatedUserInfo == null || id != updatedUserInfo.Id)
        {
            return BadRequest("User information is null or ID mismatch.");
        }

        var result = await _couchbaseService.GetAsync(id);
        var existingUserInfo = result.ContentAs<UserInfo>();

        if (existingUserInfo == null)
        {
            return NotFound("User not found.");
        }

        updatedUserInfo.Id = existingUserInfo.Id;
        updatedUserInfo.Presentations = existingUserInfo.Presentations;

        await _couchbaseService.UpsertAsync(id, updatedUserInfo);
        return Ok("User updated successfully.");
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var result = await _couchbaseService.GetAsync(id);
        if (result == null)
        {
            return NotFound("User not found.");
        }

        await _couchbaseService.RemoveAsync(id);
        return Ok("User deleted successfully.");
    }
}
