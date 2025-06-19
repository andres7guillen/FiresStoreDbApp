using FireBaseDomain.Services;
using Google.Cloud.Firestore;

namespace FireBaseDomain.Entities;

[FirestoreData]
public class Student
{
    [FirestoreProperty] 
    public string Name { get; private set; }
    [FirestoreProperty] 
    public string LastName { get; private set; }
    [FirestoreProperty] 
    public string Phone { get; private set; }
    [FirestoreProperty] 
    public int Age { get; private set; }
    [FirestoreProperty] 
    public string Email { get; private set; }
    [FirestoreProperty] 
    public string Address { get; private set; }
    [FirestoreProperty] 
    public string University { get; private set; }
    [FirestoreProperty] 
    public string Semester { get; private set; }
    [FirestoreProperty] 
    public string Time { get; private set; }
    [FirestoreProperty] 
    public string Gender { get; private set; }

    private Student() { }

    public static Student Create(
        string withName, string withLastName, string withPhone, int withAge, string withEmail,
        string withAddress, string withUniversity, string withSemester, string withTime, string withGender)
    {
        return new Student
        {
            Name = withName,
            LastName = withLastName,
            Phone = withPhone,
            Age = withAge,
            Email = withEmail,
            Address = withAddress,
            University = withUniversity,
            Semester = withSemester,
            Time = withTime,
            Gender = withGender
        };
    }

    public async Task AddTo(IFireStoreService firestoreService)
    {
        await firestoreService.SaveStudentAsync(this);
    }
}
