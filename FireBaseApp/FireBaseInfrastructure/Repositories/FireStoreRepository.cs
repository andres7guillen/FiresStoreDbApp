using CSharpFunctionalExtensions;
using FireBaseDomain.DTO;
using FireBaseDomain.Entities;
using FireBaseDomain.Repositories;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBaseInfrastructure.Repositories;

public class FireStoreRepository : IFireStoreRepository
{
    private readonly FirestoreDb _firestore;

    public FireStoreRepository(FirestoreDb firestore)
    {
        _firestore = firestore;
    }

    private const string _collectionName = "Students";    

    public async Task<List<StudentDto>> GetAllStudentsAsync()
    {
        QuerySnapshot snapshot = await _firestore.Collection(_collectionName).GetSnapshotAsync();
        var result = new List<StudentDto>();

        foreach (var doc in snapshot.Documents)
        {
            try
            {
                var student = doc.ConvertTo<Student>();
                result.Add(new StudentDto
                {
                    Id = doc.Id,
                    Name = student.Name,
                    LastName = student.LastName,
                    Phone = student.Phone,
                    Age = student.Age,
                    Email = student.Email,
                    Address = student.Address,
                    University = student.University,
                    Semester = student.Semester,
                    Time = student.Time,
                    Gender = student.Gender
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en documento {doc.Id}: {ex.Message}");
            }
        }

        return result;
    }

    public async Task<Maybe<StudentDto>> GetStudentByIdAsync(string id)
    {
        DocumentReference docRef = _firestore.Collection(_collectionName).Document(id);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            var student = snapshot.ConvertTo<Student>();
            return new StudentDto
            {
                Id = snapshot.Id,
                Name = student.Name,
                LastName = student.LastName,
                Phone = student.Phone,
                Age = student.Age,
                Email = student.Email,
                Address = student.Address,
                University = student.University,
                Semester = student.Semester,
                Time = student.Time,
                Gender = student.Gender
            };
        }
        return Maybe.None;
    }
    

    public async Task SaveStudentAsync(Student student)
    {
        await _firestore
            .Collection(_collectionName).AddAsync(student);
    }

    public async Task UpdateStudentAsync(string id, Student student)
    {
        DocumentReference docRef = _firestore.Collection(_collectionName).Document(id);
        await docRef.SetAsync(student, SetOptions.Overwrite);
    }

    public async Task DeleteStudentAsync(string id)
    {
        DocumentReference docRef = _firestore.Collection(_collectionName).Document(id);
        await docRef.DeleteAsync();
    }

    public async Task<List<StudentDto>> GetStudentsByFilterAsync(string fieldName, string fieldValue)
    {
        object valueToQuery = string.IsNullOrEmpty;
        switch (fieldName.ToLower())
        {


            case "edad" or "idUsuario" or "semestre":

                if (int.TryParse(fieldValue, out int intValue))
                {
                    valueToQuery = intValue;
                }

                break;
            case "telefono":
                if (long.TryParse(fieldValue, out long longValue))
                {
                    valueToQuery = longValue;
                }

                break;

            default:

                valueToQuery = fieldValue;
                break;
        }



        Query query = _firestore.Collection(_collectionName)
                                 .WhereEqualTo(fieldName, valueToQuery);

        QuerySnapshot snapshot = await query.GetSnapshotAsync();
        var result = new List<StudentDto>();
        foreach (var document in snapshot.Documents)
        {
            try
            {
                var student = document.ConvertTo<Student>();
                result.Add(new StudentDto
                {
                    Id = document.Id,
                    Name = student.Name,
                    LastName = student.LastName,
                    Phone = student.Phone,
                    Age = student.Age,
                    Email = student.Email,
                    Address = student.Address,
                    University = student.University,
                    Semester = student.Semester,
                    Time = student.Time,
                    Gender = student.Gender
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en documento {document.Id}: {ex.Message}");
            }
        }

        return result;
    }
}
