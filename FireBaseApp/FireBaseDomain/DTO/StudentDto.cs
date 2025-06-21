namespace FireBaseDomain.DTO;

public class StudentDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string University { get; set; } = string.Empty;
    public string Semester { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
}
