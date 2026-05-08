
using CW20.Domain;
using Microsoft.Data.SqlClient;

namespace CW20.Services;

public class RegistrationService : IRegistrationService
{
    private readonly string _connectionString;

    public RegistrationService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task CancelRegistration(int registerationId)
    {
        var user = new User();

        await using var connection = new SqlConnection(_connectionString);

        connection.Open();

        var query = @"UPDATE Registrations SET Isdeleted = 1 WHERE Id = @Id";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Id", registerationId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<List<User>> GetEventParticipants(int eventId)
    {
        var users = new List<User>();

        await using var connection = new SqlConnection(_connectionString);

        connection.Open();

        var query = @"Select * From Users as U
               WHERE U.Id IN (
               SELECT R.UserId FROM Registrations as R
               WHERE R.EventId=@EventId ) ";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@EventId", eventId);
        var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var user = new User();

            user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            user.FullName = reader.GetString(reader.GetOrdinal("FullName"));
            user.Email = reader.GetString(reader.GetOrdinal("Email"));
            user.PhoneNumber = reader.GetString(reader.GetOrdinal("Phone"));
            user.CreateAt = reader.GetDateTime(reader.GetOrdinal("RegisterDate"));
            user.IsDeleted = reader.GetBoolean(reader.GetOrdinal("Isdeleted"));

            users.Add(user);
        }

        return users;
    }

    public async Task<List<EventsTable>> GetUserEvents(int userId)
    {
        var userEvent = new List<EventsTable>();

        await using var connection = new SqlConnection(_connectionString);

        connection.Open();
        var Query = @"SELECT * FROM EventsTable ev
                      WHERE ev.id in (select EventId FROM
                      Registrations r where r.userid=@UserId)";

        var command = new SqlCommand(Query, connection);

        command.Parameters.AddWithValue("@UserId", userId);

        var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var eventTable = new EventsTable();

            eventTable.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            eventTable.Title = reader.GetString(reader.GetOrdinal("Title"));
            eventTable.Description = reader.GetString(reader.GetOrdinal("Description"));
            eventTable.Location = reader.GetString(reader.GetOrdinal("Location"));
            eventTable.Capacity = reader.GetInt32(reader.GetOrdinal("Capacity"));
            eventTable.EventDate = reader.GetDateTime(reader.GetOrdinal("EventDate"));

            userEvent.Add(eventTable);
        }

        return userEvent;
    }

    public async Task RegisterUserToEvent(int userId, int eventId)
    {
        //var Registerations = new Registration();

        await using var connection = new SqlConnection(_connectionString);

        connection.Open();

        var query = @"INSERT INTO Registrations
                      (
                          UserId,
                          EventId, 
                          RegisterDate,
                          Isdeleted
                      )
                       VALUES
                      (
                        @UserId,
                        @EventId, 
                        @RegisterDate,
                        @Isdeleted
                      
                      )";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@UserId", userId);
        command.Parameters.AddWithValue("@EventId", eventId);
        command.Parameters.AddWithValue("@RegisterDate", DateTime.UtcNow);
        command.Parameters.AddWithValue("@Isdeleted", false);

        command.ExecuteNonQuery();
    }
}
