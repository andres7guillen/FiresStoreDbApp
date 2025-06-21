using FireBaseDomain.DTO;
using FireBaseDomain.Entities;
using FireBaseDomain.Services;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _firestoreService.GetAllStudentsAsync();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var student = await _firestoreService.GetStudentByIdAsync(id);
        if (student == null) return NotFound();
        return Ok(student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] StudentDto dto)
    {
        var student = Student.Create(
            dto.Name, dto.LastName, dto.Phone, dto.Age, dto.Email,
            dto.Address, dto.University, dto.Semester, dto.Time, dto.Gender
        );
        await _firestoreService.UpdateStudentAsync(id, student);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _firestoreService.DeleteStudentAsync(id);
        return NoContent();
    }

}
