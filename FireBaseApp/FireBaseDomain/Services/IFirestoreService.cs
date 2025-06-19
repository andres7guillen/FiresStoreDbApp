using FireBaseDomain.Entities;
using Microsoft.AspNetCore.Http;

namespace FireBaseDomain.Services;

public interface IFireStoreService
{
    Task ImportStudentsFromCsvAsync(IFormFile file);
    Task SaveStudentAsync(Student student);
}
