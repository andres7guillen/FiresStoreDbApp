using FireBaseDomain.Entities;
using FireBaseDomain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;

namespace FireBaseApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FireBaseController : ControllerBase
{
    private readonly IFireStoreService _firestoreService;

    public FireBaseController(IFireStoreService firestoreService)
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
            await _firestoreService.ImportStudentsFromCsvAsync(file);
            return Ok("Estudiantes cargados exitosamente.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error procesando el archivo: {ex.Message}");
        }
    }

}
