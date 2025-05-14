using Concycle.Business.Interfaces;
using Concycle.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Concycle.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConRequestsController : ControllerBase
{
    private readonly IConRequestService _conRequestService;

    public ConRequestsController(IConRequestService conRequestService)
    {
        _conRequestService = conRequestService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ConRequestDto>>> GetAllConRequests()
    {
        var list = await _conRequestService.GetRequests();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ConRequestDto>> GetConRequestById(Guid id)
    {
        var request = await _conRequestService.GetRequestById(id);
        if (request is null) return NotFound();
        return Ok(request);
    }

    [HttpGet("post/{postId}")]
    public async Task<ActionResult<List<ConRequestDto>>> GetConRequestByPost(Guid postId)
    {
        var list = await _conRequestService.GetRequestsByPost(postId);
        return Ok(list);
    }

    [HttpGet("applicant/{userId}")]
    public async Task<ActionResult<List<ConRequestDto>>> GetConRequestByApplicant(Guid userId)
    {
        var list = await _conRequestService.GetRequestsByApplicant(userId);
        return Ok(list);
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<List<ConRequestDto>>> GetConRequestByStatus(string status)
    {
        var list = await _conRequestService.GetRequestsByStatus(status);
        return Ok(list);
    }

    [HttpPost]
    public async Task<ActionResult<ConRequestDto>> CreateConRequest([FromBody] CreateConRequestDto dto)
    {
        var created = await _conRequestService.CreateRequest(dto);
        return CreatedAtAction(nameof(GetConRequestById), new { id = created.Id }, created);
    }

    [HttpPut("{id}/status")]
    public async Task<ActionResult<ConRequestDto>> UpdateStatus(Guid id, [FromQuery] string status)
    {
        var updated = await _conRequestService.UpdateStatus(id, status);
        if (updated is null) return BadRequest("Başvuru güncellenemedi.");
        return Ok(updated);
    }

    [HttpPut("{id}/complete")]
    public async Task<ActionResult<ConRequestDto>> CompleteConRequest(Guid id)
    {
        var completed = await _conRequestService.CompleteRequest(id);
        if (completed is null)
            return BadRequest("Başvuru tamamlanamadı. Belki de zaten tamamlandı ya da henüz kabul edilmedi.");
        return Ok(completed);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteConRequest(Guid id)
    {
        var success = await _conRequestService.DeleteRequest(id);
        return success ? NoContent() : NotFound();
    }
}
