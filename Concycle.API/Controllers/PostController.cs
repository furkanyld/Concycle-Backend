using Concycle.Business.Interfaces;
using Concycle.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Concycle.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PostDto>>> GetAllPosts()
    {
        var posts = await _postService.GetPosts();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetPostById(Guid id)
    {
        var post = await _postService.GetPostById(id);
        if (post is null) return NotFound();
        return Ok(post);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<PostDto>>> GetPostByUser(Guid userId)
    {
        var posts = await _postService.GetPostsByUser(userId);
        return Ok(posts);
    }

    [HttpGet("type/{type}")]
    public async Task<ActionResult<List<PostDto>>> GetPostByType(string type)
    {
        var posts = await _postService.GetPostsByType(type);
        return Ok(posts);
    }

    [HttpGet("category/{category}")]
    public async Task<ActionResult<List<PostDto>>> GetPostByCategory(string category)
    {
        var posts = await _postService.GetPostsByCategory(category);
        return Ok(posts);
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> CreatePost([FromBody] PostDto postDto)
    {
        var created = await _postService.CreatePost(postDto);
        if (created is null)
            return BadRequest("Katkı puanınız bu ilanı açmaya yetmiyor.");
        return CreatedAtAction(nameof(GetPostById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PostDto>> UpdatePost(Guid id, [FromBody] PostDto postDto)
    {
        var updated = await _postService.UpdatePost(id, postDto);
        if (updated is null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePost(Guid id)
    {
        var success = await _postService.DeletePost(id);
        return success ? NoContent() : NotFound();
    }
}
