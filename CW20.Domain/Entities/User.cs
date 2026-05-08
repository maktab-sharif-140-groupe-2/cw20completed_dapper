namespace CW20.Domain;

public class User : BaseEntity
{
    public User(string fullName, string email, string phoneNumber)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public User() { }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }


    protected override void Validate()
    {
        throw new NotImplementedException();
    }
}
