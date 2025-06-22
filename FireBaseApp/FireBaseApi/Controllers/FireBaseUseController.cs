using FireBaseDomain.DTO;
using FireBaseDomain.Entities;
using FireBaseDomain.Services;
using Microsoft.AspNetCore.Mvc;

namespace FireBaseApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FireBaseUseController : ControllerBase
{
    private readonly IFireStoreServiceUsu _firestoreService;

    public FireBaseUseController(IFireStoreServiceUsu firestoreService)
    {
        _firestoreService = firestoreService;
    }

    [HttpPost("upload-csv")]
    public async Task<IActionResult> UploadCsv(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Archivo CSV no proporcionado o vacío.");

        try
        {
            await _firestoreService.ImportUsuarioFromCsvAsync(file);
            return Ok("Estudiantes cargados exitosamente.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error procesando el archivo: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usuario = await _firestoreService.GetAllUsuariosAsync();
        return Ok(usuario);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var usuario = await _firestoreService.GetUsuarioByIdAsync(id);
        if (usuario == null) return NotFound();
        return Ok(usuario);
    }

    

    [HttpGet("filter")] // Ruta específica para la búsqueda con filtros
    public async Task<IActionResult> GetSomeFilters([FromQuery] string fieldName, [FromQuery] string fieldValue)
    {
        var usuarioResult = await _firestoreService.GetUsuariosBySomeFieldFilterAsync(fieldName, fieldValue);

        if (usuarioResult == null)
        {
            
            return StatusCode(500, "Error al obtener los usuarios.");
        }

        if (usuarioResult.Count == 0) 
        {
            return NotFound(); 
        }

        return Ok(usuarioResult); 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UsuarioDto dto)
    {
        var usuarioCambio = usuario.Create(
            dto.apellido
            , dto.correo
            , dto.direccion
            , dto.edad
            , dto.genero
            , dto.idUsuario
            , dto.jornada
            , dto.nombre
            , dto.semestre
            , dto.telefono
            , dto.universidad


        );
        await _firestoreService.UpdateUsuarioAsync(id, usuarioCambio);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _firestoreService.DeleteUsuarioAsync(id);
        return NoContent();
    }

}


