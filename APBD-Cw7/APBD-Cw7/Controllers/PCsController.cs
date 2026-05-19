using APBD_Cw7.DTOs.Requests;
using APBD_Cw7.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Cw7.Controllers;
[ApiController]
[Route("api/pcs")]
public class PCsController : ControllerBase
{
    private readonly IPCsService _service;

    public PCsController(IPCsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}/components")]
    public async Task<IActionResult> GetComponents(int id)
    {
        var result = await _service.GetComponentsAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PCCreateRequest dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PCUpdateRequest dto)
    {
        var updated = await _service.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}