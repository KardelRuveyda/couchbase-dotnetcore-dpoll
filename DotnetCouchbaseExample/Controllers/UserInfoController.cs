using DotnetCouchbaseExample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UserInfoController : ControllerBase
{
    private readonly UserInfoService _couchbaseService;

    public UserInfoController(UserInfoService couchbaseService)
    {
        _couchbaseService = couchbaseService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserInfo userInfo)
    {
        var uniqueId = Guid.NewGuid().ToString();
        userInfo.Id = uniqueId;
        await _couchbaseService.InsertAsync(uniqueId, userInfo);
        return Ok(new { Id = uniqueId });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UserInfo userInfo)
    {
        userInfo.Id = id;
        await _couchbaseService.UpsertAsync(id, userInfo);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await _couchbaseService.GetAsync(id);
        return Ok(result.ContentAs<UserInfo>());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _couchbaseService.RemoveAsync(id);
        return Ok();
    }
}
