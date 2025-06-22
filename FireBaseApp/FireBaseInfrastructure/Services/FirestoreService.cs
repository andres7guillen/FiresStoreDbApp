using CSharpFunctionalExtensions;
using FireBaseDomain.DTO;
using FireBaseDomain.Entities;
using FireBaseDomain.Repositories;
using FireBaseDomain.Services;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace FireBaseInfrastructure.Services;

public class FireStoreService : IFireStoreService
{
    private readonly IFireStoreRepository _repository;
    public FireStoreService(IConfiguration configuration, FirestoreDb firestore, IFireStoreRepository repository)
    {
        _repository = repository;
    }

    public async Task ImportStudentsFromCsvAsync(IFormFile file)
    {
        using var stream = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
        bool isFirstLine = true;

        while (!stream.EndOfStream)
        {
            var line = await stream.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line)) continue;

            if (isFirstLine)
            {
                isFirstLine = false;
                continue;
            }

            var values = line.Split(',');

            if (values.Length != 10)
                throw new InvalidOperationException("El archivo CSV tiene un formato incorrecto. Se esperaban 10 columnas.");

            var name = values[0].Trim();
            var lastName = values[1].Trim();
            var phone = values[2].Trim();
            var age = int.TryParse(values[3], out int parsedAge) ? parsedAge : 0;
            var email = values[4].Trim();
            var address = values[5].Trim();
            var university = values[6].Trim();
            var semester = values[7].Trim();
            var time = values[8].Trim();
            var gender = values[9].Trim();

            var student = Student.Create(
                withName: name,
                withLastName: lastName,
                withPhone: phone,
                withAge: age,
                withEmail: email,
                withAddress: address,
                withUniversity: university,
                withSemester: semester,
                withTime: time,
                withGender: gender
            );

            await SaveStudentAsync(student);
        }
    }

    public async Task SaveStudentAsync(Student student)
    {
        await _repository.SaveStudentAsync(student);
    }

    public async Task<List<StudentDto>> GetAllStudentsAsync()
    {
        return await _repository.GetAllStudentsAsync();
    }

    public async Task<Maybe<StudentDto>> GetStudentByIdAsync(string id)
    {
        return await _repository.GetStudentByIdAsync(id);
    }

    public async Task UpdateStudentAsync(string id, Student student)
    {
        await _repository.UpdateStudentAsync(id, student);
    }

    public async Task DeleteStudentAsync(string id)
    {
        await _repository.DeleteStudentAsync(id);
    }
}
