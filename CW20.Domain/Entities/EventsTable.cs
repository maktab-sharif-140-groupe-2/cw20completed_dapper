namespace CW20.Domain;

public class EventsTable : BaseEntity
{
    public EventsTable()
    {
        
    }

    public EventsTable(string title, string description, string location, int capacity, DateTime eventDate)
    {
        Title = title;
        Description = description;
        Location = location;
        Capacity = capacity;
        EventDate = eventDate;
    }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Location { get; set; }

    public int Capacity { get; set; }

    public DateTime EventDate { get; set; }


    protected override void Validate()
    {
        throw new NotImplementedException();
    }
}
