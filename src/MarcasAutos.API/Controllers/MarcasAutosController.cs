using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarcasAutos.Application.DTOs;
using MarcasAutos.Application.Services;

namespace MarcasAutos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarcasAutosController : ControllerBase
{
    private readonly IMarcaAutoService _service;

    public MarcasAutosController(IMarcaAutoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MarcaAutoDto>>> Get()
    {
        var marcas = await _service.GetAllMarcasAsync();
        return Ok(marcas);
    }
}
