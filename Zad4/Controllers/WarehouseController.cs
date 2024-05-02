using Microsoft.AspNetCore.Mvc;
using Zad4.DTOs;
using Zad4.Exceptions;
using Zad4.Services;

namespace Zad4.Controllers;

[ApiController]
[Route("api/warehouse")]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterProduct([FromBody] WarehouseDto dto)
    {
        try
        {
            var createdId = await _warehouseService.RegisterProductAsync(dto);
            return Ok(createdId);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }
}