namespace CW20.Domain;

public class Registration : BaseEntity
{
    public Registration(int userId, int eventId, bool isdeleted, DateTime registerDate)
    {
        UserId = userId;

        EventId = eventId;

        RegisterDate = registerDate;

    }

    public int UserId { get; set; }

    public  int  EventId { get; set; }

    public DateTime RegisterDate { get; set; }

    protected override void Validate()
    {
        throw new NotImplementedException();
    }
}
