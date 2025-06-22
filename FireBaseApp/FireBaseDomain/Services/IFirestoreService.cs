using CSharpFunctionalExtensions;
using FireBaseDomain.DTO;
using FireBaseDomain.Entities;
using Microsoft.AspNetCore.Http;

namespace FireBaseDomain.Services;

public interface IFireStoreService
{
    Task ImportStudentsFromCsvAsync(IFormFile file);
    Task SaveStudentAsync(Student student);
    Task<List<StudentDto>> GetAllStudentsAsync();
    Task<Maybe<StudentDto>> GetStudentByIdAsync(string id); 
    Task UpdateStudentAsync(string id, Student student); 
    Task DeleteStudentAsync(string id);
    Task<List<StudentDto>> GetStudentsByFilterAsync(string fieldName, string fieldValue);
}
